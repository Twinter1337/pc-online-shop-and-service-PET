using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

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
            List<Service> services = await _servicesCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceDto>>(services));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _servicesCrudService.GetEntityByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServiceDto>(service));
        }
        
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<ServiceDto>> GetServiceByName(string name)
        {
            ServicesCrudService servicesCrudService = (_servicesCrudService as ServicesCrudService)!;
            var service = await servicesCrudService.GetServiceByNameAsync(name);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServiceDto>(service));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDto serviceDto)
        {
            if (id != serviceDto.ServiceId)
            {
                return BadRequest();
            }

            bool result = await _servicesCrudService.UpdateEntityAsync(id, _mapper.Map<Service>(serviceDto));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> PostService(ServiceDto serviceDto)
        {
            var createdService = _mapper.Map<Service>(serviceDto);
            bool result = await _servicesCrudService.CreateEntityAsync(createdService);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetService), new { id = createdService.ServiceId },
                _mapper.Map<ServiceDto>(createdService));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
           bool result = await _servicesCrudService.DeleteEntityAsync(id);
           if (!result)
           {
               return NotFound();
           }
           
           return NoContent();
        }
    }
}