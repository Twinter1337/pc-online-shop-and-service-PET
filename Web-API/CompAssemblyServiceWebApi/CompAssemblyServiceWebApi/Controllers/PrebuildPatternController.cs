using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrebuildPatternController : ControllerBase
    {
        private readonly ICrudService<PrebuildPattern> _prebuildPatternCrudService;
        private readonly IMapper _mapper;

        public PrebuildPatternController(ICrudService<PrebuildPattern> prebuildPatternCrudService, IMapper mapper)
        {
            _prebuildPatternCrudService = prebuildPatternCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrebuildPatternDto>>> GetPrebuildPatterns()
        {
            try
            {
                var patterns = await _prebuildPatternCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<PrebuildPatternDto>>(patterns));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting patterns: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PrebuildPatternDto>>> GetFilteredPrebuildPatterns([FromQuery] PrebuildPatternFilter filter)
        {
            try
            {
                if (_prebuildPatternCrudService is not PrebuildPatternsCrudService patternsService)
                    return StatusCode(500, "Cannot cast to PrebuildPatternsCrudService");

                var filtered = await patternsService.GetFilteredPrebuildPatternsAsync(filter);
                return Ok(_mapper.Map<IEnumerable<PrebuildPatternDto>>(filtered));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error filtering patterns: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrebuildPatternDto>> GetPrebuildPattern(int id)
        {
            try
            {
                var pattern = await _prebuildPatternCrudService.GetEntityByIdAsync(id);
                if (pattern == null)
                    return NotFound($"Pattern with ID {id} not found");

                return Ok(_mapper.Map<PrebuildPatternDto>(pattern));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving pattern: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrebuildPattern(int id, PrebuildPatternUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _prebuildPatternCrudService.UpdateEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to update pattern with ID {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating pattern: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPrebuildPattern(int id, PrebuildPatternPatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _prebuildPatternCrudService.PatchEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to patch pattern with ID {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching pattern: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PrebuildPatternDto>> PostPrebuildPattern(PrebuildPatternCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var entity = _mapper.Map<PrebuildPattern>(dto);
                var result = await _prebuildPatternCrudService.CreateEntityAsync(entity);
                if (!result)
                    return BadRequest("Failed to create pattern");

                return CreatedAtAction(nameof(GetPrebuildPattern), new { id = entity.SerialNumber },
                    _mapper.Map<PrebuildPatternDto>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating pattern: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrebuildPattern(int id)
        {
            try
            {
                var result = await _prebuildPatternCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound($"Pattern with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting pattern: {ex.Message}");
            }
        }
    }
}