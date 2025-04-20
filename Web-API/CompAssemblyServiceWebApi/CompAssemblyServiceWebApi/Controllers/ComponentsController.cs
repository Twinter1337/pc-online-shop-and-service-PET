using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace ComputerAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly ICrudService<Component> _componentsCrudService;
        private readonly IMapper _mapper;

        public ComponentsController(ICrudService<Component> componentsCrudService, IMapper mapper)
        {
            _componentsCrudService = componentsCrudService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetComponents()
        {
            List<Component> components = await _componentsCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetFilteredComponents([FromQuery]ComponentFilter filter)
        {
            ComponentsCrudService componentsCrudService = (_componentsCrudService as ComponentsCrudService)!;
            List<Component> components = await componentsCrudService.GetFilteredComponentsAsync(filter);
            return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            var component = await _componentsCrudService.GetEntityByIdAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ComponentDto>(component));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, ComponentDto componentDto)
        {
            if (id != componentDto.ComponentId)
            {
                return BadRequest();
            }

            bool result = await _componentsCrudService.UpdateEntityAsync(id, _mapper.Map<Component>(componentDto));

            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<ComponentDto>> PostComponent(ComponentDto componentDto)
        {
            var createdComponent = _mapper.Map<Component>(componentDto);
            bool result = await _componentsCrudService.CreateEntityAsync(createdComponent);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetComponent), new { id = createdComponent.ComponentId },
                _mapper.Map<ComponentDto>(createdComponent));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            bool result = await _componentsCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }
        
            return NoContent();
        }
    }
}
