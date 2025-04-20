using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

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
            List<PatternComponent> patternComponents = await _patternComponentCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<PatternComponentDto>>(patternComponents));
        }

        [HttpGet("by-prebuild/{prebuildId}")]
        public async Task<ActionResult<IEnumerable<PatternComponentDto>>> GetPatternComponentsByPrebuildPatternId(
            int prebuildId)
        {
            PatternComponentsCrudService patternComponentCrudService =
                (_patternComponentCrudService as PatternComponentsCrudService)!;
            List<PatternComponent> patternComponents =
                await patternComponentCrudService.GetPatternComponentsByPrebuildPatternIdAsync(prebuildId);
            
            return Ok(_mapper.Map<IEnumerable<PatternComponentDto>>(patternComponents));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatternComponentDto>> GetPatternComponent(int id)
        {
            var patternComponent = await _patternComponentCrudService.GetEntityByIdAsync(id);

            if (patternComponent == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PatternComponentDto>(patternComponent));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatternComponent(int id, PatternComponentDto patternComponentDto)
        {
            if (id != patternComponentDto.PatternComponentId)
            {
                return BadRequest();
            }

            bool result =
                await _patternComponentCrudService.UpdateEntityAsync(id,
                    _mapper.Map<PatternComponent>(patternComponentDto));

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PatternComponentDto>> PostPatternComponent(
            PatternComponentDto patternComponentDto)
        {
            var createdPatternComponent = _mapper.Map<PatternComponent>(patternComponentDto);
            bool result = await _patternComponentCrudService.CreateEntityAsync(createdPatternComponent);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetPatternComponent), new { id = createdPatternComponent.PatternComponentId },
                _mapper.Map<PatternComponentDto>(createdPatternComponent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatternComponent(int id)
        {
            bool result = await _patternComponentCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}