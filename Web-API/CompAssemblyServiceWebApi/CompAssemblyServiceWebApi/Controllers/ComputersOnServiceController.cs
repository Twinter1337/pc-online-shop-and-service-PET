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
    public class ComputersOnServiceController : ControllerBase
    {
        private readonly ICrudService<ComputerOnService> _computerOnServiceCrudService;
        private readonly IMapper _mapper;

        public ComputersOnServiceController(ICrudService<ComputerOnService> computerOnServiceCrudService,
            IMapper mapper)
        {
            _computerOnServiceCrudService = computerOnServiceCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerOnServiceDto>>> GetComputersOnService()
        {
            List<ComputerOnService> computerOnServices = await _computerOnServiceCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computerOnServices));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ComputerOnServiceDto>>> GetFilteredComputersOnService(
            [FromQuery] ComputerOnServiceFilter filter)
        {
            ComputersOnCrudServiceCrudService computersOnServiceCrudService =
                (_computerOnServiceCrudService as ComputersOnCrudServiceCrudService)!;
            List<ComputerOnService> computersOnService =
                await computersOnServiceCrudService.GetFilteredComputersOnServiceAsync(filter);
            return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computersOnService));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<ComputerOnServiceDto>>> GetFilteredComputersOnService(int userId)
        {
            ComputersOnCrudServiceCrudService computersOnServiceCrudService =
                (_computerOnServiceCrudService as ComputersOnCrudServiceCrudService)!;
            var computersOnService = await computersOnServiceCrudService.GetComputersOnServiceByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computersOnService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerOnServiceDto>> GetComputerOnService(int id)
        {
            var computerOnService = await _computerOnServiceCrudService.GetEntityByIdAsync(id);

            if (computerOnService == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ComputerOnServiceDto>(computerOnService));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerOnService(int id, ComputerOnServiceDto computerOnServiceDto)
        {
            if (id != computerOnServiceDto.ComputerOnServiceId)
            {
                return BadRequest();
            }

            bool result = await _computerOnServiceCrudService.UpdateEntityAsync(id,
                _mapper.Map<ComputerOnService>(computerOnServiceDto));

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ComputerOnServiceDto>> PostComputerOnService(
            ComputerOnServiceDto computerOnServiceDto)
        {
            var createdComputerOnService = _mapper.Map<ComputerOnService>(computerOnServiceDto);
            bool result = await _computerOnServiceCrudService.CreateEntityAsync(createdComputerOnService);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetComputerOnService),
                new { id = createdComputerOnService.ComputerOnServiceId },
                _mapper.Map<ComputerOnServiceDto>(createdComputerOnService));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerOnService(int id)
        {
            bool result = await _computerOnServiceCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}