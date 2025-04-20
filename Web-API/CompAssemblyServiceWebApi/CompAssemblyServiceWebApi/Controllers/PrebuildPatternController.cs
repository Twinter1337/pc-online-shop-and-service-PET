using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

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
            List<PrebuildPattern> prebuildPatterns = await _prebuildPatternCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<PrebuildPatternDto>>(prebuildPatterns));
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PrebuildPatternDto>>> GetFilteredPrebuildPatterns([FromQuery] PrebuildPatternFilter filter)
        {
            PrebuildPatternsCrudService prebuildPatternsCrudService = (_prebuildPatternCrudService as PrebuildPatternsCrudService)!;
            List<PrebuildPattern> prebuildPatterns = await prebuildPatternsCrudService.GetFilteredPrebuildPatternsAsync(filter);
            return Ok(_mapper.Map<IEnumerable<PrebuildPatternDto>>(prebuildPatterns));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrebuildPatternDto>> GetPrebuildPattern(int id)
        {
            var prebuildPattern = await _prebuildPatternCrudService.GetEntityByIdAsync(id);

            if (prebuildPattern == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PrebuildPatternDto>(prebuildPattern));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrebuildPattern(int id, PrebuildPatternDto prebuildPatternDto)
        {
            if (id != prebuildPatternDto.SerialNumber)
            {
                return BadRequest();
            }

            bool result =
                await _prebuildPatternCrudService.UpdateEntityAsync(id,
                    _mapper.Map<PrebuildPattern>(prebuildPatternDto));

            if (!result)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<PrebuildPatternDto>> PostPrebuildPattern(PrebuildPatternDto prebuildPatternDto)
        {
            var createdPrebuildPattern = _mapper.Map<PrebuildPattern>(prebuildPatternDto);
            bool result = await _prebuildPatternCrudService.CreateEntityAsync(createdPrebuildPattern);

            if (!result)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetPrebuildPattern), new { id = createdPrebuildPattern.SerialNumber },
                _mapper.Map<PrebuildPattern>(createdPrebuildPattern));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrebuildPattern(int id)
        {
            bool result = await _prebuildPatternCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
    }
}