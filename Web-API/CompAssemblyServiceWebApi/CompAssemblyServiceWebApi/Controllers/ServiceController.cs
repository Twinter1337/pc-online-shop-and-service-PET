using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ServiceDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ICrudService<Service> _servicesCrudService;
        private readonly IMapper _mapper;

        public ServiceController(ICrudService<Service> servicesCrudService, IMapper mapper)
        {
            _servicesCrudService = servicesCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            try
            {
                var services = await _servicesCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<ServiceDto>>(services));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching services: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            try
            {
                var service = await _servicesCrudService.GetEntityByIdAsync(id);
                if (service == null)
                    return NotFound();

                return Ok(_mapper.Map<ServiceDto>(service));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching service: {ex.Message}");
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<ServiceDto>> GetServiceByName(string name)
        {
            try
            {
                var servicesCrudService = _servicesCrudService as ServicesCrudService;
                if (servicesCrudService == null)
                    return StatusCode(500, "Service logic not available");

                var service = await servicesCrudService.GetServiceByNameAsync(name);
                if (service == null)
                    return NotFound();

                return Ok(_mapper.Map<ServiceDto>(service));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching service by name: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceUpdateDto serviceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var result = await _servicesCrudService.UpdateEntityAsync(id, serviceUpdateDto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating service: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchService(int id, ServicePatchDto servicePatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var result = await _servicesCrudService.PatchEntityAsync(id, servicePatchDto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching service: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> PostService(ServiceCreateDto serviceCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var createdService = _mapper.Map<Service>(serviceCreateDto);
                var result = await _servicesCrudService.CreateEntityAsync(createdService);

                if (!result)
                    return BadRequest("Failed to create service");

                return CreatedAtAction(nameof(GetService), new { id = createdService.ServiceId },
                    _mapper.Map<ServiceDto>(createdService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating service: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var result = await _servicesCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting service: {ex.Message}");
            }
        }
    }
}