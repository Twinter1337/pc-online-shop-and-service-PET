using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrebuildPatternController : ControllerBase
    {
        private readonly ICrudService<PrebuildPattern> _service;
        private readonly IMapper _mapper;
        private readonly PrebuildPatternsCrudService _ppcs;

        public PrebuildPatternController(ICrudService<PrebuildPattern> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _ppcs = service as PrebuildPatternsCrudService ?? throw new InvalidCastException("Service must be PrebuildPatternsCrudService");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrebuildPatternDto>>> GetAll()
        {
            try
            {
                var patterns = await _service.GetAllEntitiesAsync();
                return Ok(BuildDtos(patterns));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting patterns: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PrebuildPatternDto>>> GetFiltered([FromQuery] PrebuildPatternFilter filter)
        {
            try
            {
                var filtered = await _ppcs.GetFilteredPrebuildPatternsAsync(filter);
                return Ok(BuildDtos(filtered));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error filtering patterns: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrebuildPatternDto>> GetById(int id)
        {
            try
            {
                var pattern = await _service.GetEntityByIdAsync(id);
                if (pattern == null) return NotFound($"Pattern with ID {id} not found");

                return Ok(BuildDtos(new[] { pattern }).First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving pattern: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PrebuildPatternDto>> Create(PrebuildPatternCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var entity = _mapper.Map<PrebuildPattern>(dto);
                if (!await _service.CreateEntityAsync(entity))
                    return BadRequest("Failed to create pattern");

                return CreatedAtAction(nameof(GetById), new { id = entity.SerialNumber }, _mapper.Map<PrebuildPatternDto>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating pattern: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PrebuildPatternUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return await _service.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : BadRequest($"Failed to update pattern with ID {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating pattern: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, PrebuildPatternPatchDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return await _service.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : BadRequest($"Failed to patch pattern with ID {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching pattern: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _service.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Pattern with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting pattern: {ex.Message}");
            }
        }

        private List<PrebuildPatternDto> BuildDtos(IEnumerable<PrebuildPattern> patterns)
        {
            var dtos = _mapper.Map<List<PrebuildPatternDto>>(patterns);
            foreach (var (pattern, dto) in patterns.Zip(dtos))
            {
                dto.Components = _ppcs.GetComponentsForPattern(pattern, _mapper);
            }
            return dtos;
        }
    }
}
