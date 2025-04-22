using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatternComponentDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatternComponentController : ControllerBase
    {
        private readonly ICrudService<PatternComponent> _patternComponentCrudService;
        private readonly IMapper _mapper;

        public PatternComponentController(ICrudService<PatternComponent> patternComponentCrudService, IMapper mapper)
        {
            _patternComponentCrudService = patternComponentCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatternComponentDto>>> GetPatternComponents()
        {
            try
            {
                var patternComponents = await _patternComponentCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<PatternComponentDto>>(patternComponents));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-prebuild/{prebuildId}")]
        public async Task<ActionResult<IEnumerable<PatternComponentDto>>> GetPatternComponentsByPrebuildPatternId(int prebuildId)
        {
            try
            {
                var patternComponentService = _patternComponentCrudService as PatternComponentsCrudService;
                if (patternComponentService == null)
                    return StatusCode(500, "Internal service error: Unable to cast to PatternComponentsCrudService");

                var patternComponents = await patternComponentService.GetPatternComponentsByPrebuildPatternIdAsync(prebuildId);
                return Ok(_mapper.Map<IEnumerable<PatternComponentDto>>(patternComponents));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatternComponentDto>> GetPatternComponent(int id)
        {
            try
            {
                var patternComponent = await _patternComponentCrudService.GetEntityByIdAsync(id);
                if (patternComponent == null)
                    return NotFound($"PatternComponent with ID {id} not found.");

                return Ok(_mapper.Map<PatternComponentDto>(patternComponent));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatternComponent(int id, PatternComponentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.PatternId != null)
                {
                    var ppcs = new PrebuildPatternsCrudService(_patternComponentCrudService.Context);
                    var pb = await ppcs.GetEntityByIdAsync(dto.PatternId);
                    if (pb == null)
                        return NotFound($"Prebuild pattern with ID {dto.PatternId} not found.");
                }

                if (dto.ComponentId != null)
                {
                    var ccs = new ComponentsCrudService(_patternComponentCrudService.Context);
                    var component = await ccs.GetEntityByIdAsync(dto.ComponentId);
                    if (component == null)
                        return NotFound($"Component with ID {dto.ComponentId} not found.");
                }

                var result = await _patternComponentCrudService.UpdateEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to update PatternComponent with ID {id}.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPatternComponent(int id, PatternComponentPatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.PatternId != null)
                {
                    var ppcs = new PrebuildPatternsCrudService(_patternComponentCrudService.Context);
                    var pb = await ppcs.GetEntityByIdAsync((int)dto.PatternId);
                    if (pb == null)
                        return NotFound($"Prebuild pattern with ID {dto.PatternId} not found.");
                }

                if (dto.ComponentId != null)
                {
                    var ccs = new ComponentsCrudService(_patternComponentCrudService.Context);
                    var component = await ccs.GetEntityByIdAsync((int)dto.ComponentId);
                    if (component == null)
                        return NotFound($"Component with ID {dto.ComponentId} not found.");
                }

                var result = await _patternComponentCrudService.PatchEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to patch PatternComponent with ID {id}.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PatternComponentDto>> PostPatternComponent(PatternComponentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.PatternId != null)
                {
                    var ppcs = new PrebuildPatternsCrudService(_patternComponentCrudService.Context);
                    var pb = await ppcs.GetEntityByIdAsync(dto.PatternId);
                    if (pb == null)
                        return NotFound($"Prebuild pattern with ID {dto.PatternId} not found.");
                }

                if (dto.ComponentId != null)
                {
                    var ccs = new ComponentsCrudService(_patternComponentCrudService.Context);
                    var component = await ccs.GetEntityByIdAsync(dto.ComponentId);
                    if (component == null)
                        return NotFound($"Component with ID {dto.ComponentId} not found.");
                }

                var created = _mapper.Map<PatternComponent>(dto);
                var result = await _patternComponentCrudService.CreateEntityAsync(created);

                if (!result)
                    return BadRequest("Failed to create PatternComponent.");

                return CreatedAtAction(nameof(GetPatternComponent), new { id = created.PatternComponentId },
                    _mapper.Map<PatternComponentDto>(created));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatternComponent(int id)
        {
            try
            {
                var result = await _patternComponentCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound($"PatternComponent with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}