using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

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
            try
            {
                List<Component> components = await _componentsCrudService.GetAllEntitiesAsync();
                if (components == null || !components.Any())
                {
                    return NotFound("No components found.");
                }
                return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetFilteredComponents([FromQuery] ComponentFilter filter)
        {
            try
            {
                ComponentsCrudService componentsCrudService = (_componentsCrudService as ComponentsCrudService)!;
                List<Component> components = await componentsCrudService.GetFilteredComponentsAsync(filter);
                if (components == null || !components.Any())
                {
                    return NotFound("No components match the filter criteria.");
                }
                return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            try
            {
                var component = await _componentsCrudService.GetEntityByIdAsync(id);

                if (component == null)
                {
                    return NotFound($"Component with ID {id} not found.");
                }

                return Ok(_mapper.Map<ComponentDto>(component));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, ComponentUpdateDto componentUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                bool result = await _componentsCrudService.UpdateEntityAsync(id, componentUpdateDto);

                if (!result)
                {
                    return NotFound($"Component with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchComponent(int id, [FromBody] ComponentPatchDto componentPatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                bool result = await _componentsCrudService.PatchEntityAsync(id, componentPatchDto);

                if (!result)
                {
                    return NotFound($"Component with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> PostComponent(ComponentCreateDto componentCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var createdComponent = _mapper.Map<Component>(componentCreateDto);
                bool result = await _componentsCrudService.CreateEntityAsync(createdComponent);

                if (!result)
                {
                    return BadRequest("Error creating the component.");
                }

                return CreatedAtAction(nameof(GetComponent), new { id = createdComponent.ComponentId },
                    _mapper.Map<ComponentDto>(createdComponent));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            try
            {
                bool result = await _componentsCrudService.DeleteEntityAsync(id);

                if (!result)
                {
                    return NotFound($"Component with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}