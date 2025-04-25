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
        private readonly ICrudService<Component> _crud;
        private readonly IMapper _mapper;

        public ComponentsController(ICrudService<Component> crud, IMapper mapper)
        {
            _crud = crud;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetComponents()
        {
            try
            {
                var components = await _crud.GetAllEntitiesAsync();
                return components?.Any() == true
                    ? Ok(_mapper.Map<IEnumerable<ComponentDto>>(components))
                    : NotFound("No components found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving components: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetFilteredComponents([FromQuery] ComponentFilter filter)
        {
            try
            {
                if (_crud is not ComponentsCrudService service)
                    return StatusCode(500, "Service not available.");

                var components = await service.GetFilteredComponentsAsync(filter);
                return components?.Any() == true
                    ? Ok(_mapper.Map<IEnumerable<ComponentDto>>(components))
                    : NotFound("No components match the filter criteria.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error filtering components: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            try
            {
                var component = await _crud.GetEntityByIdAsync(id);
                return component != null
                    ? Ok(_mapper.Map<ComponentDto>(component))
                    : NotFound($"Component with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving component: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, ComponentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (_crud is not ComponentsCrudService service)
                    return StatusCode(500, "Service not available.");

                return await service.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Component with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating component: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchComponent(int id, ComponentPatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _crud.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Component with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching component: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> PostComponent(ComponentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var component = _mapper.Map<Component>(dto);
                return await _crud.CreateEntityAsync(component)
                    ? CreatedAtAction(nameof(GetComponent), new { id = component.ComponentId }, _mapper.Map<ComponentDto>(component))
                    : BadRequest("Failed to create component.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating component: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            try
            {
                return await _crud.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Component with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting component: {ex.Message}");
            }
        }
    }
}