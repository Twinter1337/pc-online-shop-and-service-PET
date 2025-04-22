using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComputerOnServiceDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

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
            try
            {
                List<ComputerOnService> computerOnServices = await _computerOnServiceCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computerOnServices));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ComputerOnServiceDto>>> GetFilteredComputersOnService(
            [FromQuery] ComputerOnServiceFilter filter)
        {
            try
            {
                ComputersOnServiceCrudService computerOnServiceCrudService =
                    (_computerOnServiceCrudService as ComputersOnServiceCrudService)!;
                var computersOnService = await computerOnServiceCrudService.GetFilteredComputersOnServiceAsync(filter);
                return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computersOnService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<ComputerOnServiceDto>>> GetFilteredComputersOnService(int userId)
        {
            try
            {
                ComputersOnServiceCrudService computerOnServiceCrudService =
                    (_computerOnServiceCrudService as ComputersOnServiceCrudService)!;
                var computersOnService = await computerOnServiceCrudService.GetComputersOnServiceByUserIdAsync(userId);
                if (computersOnService == null || !computersOnService.Any())
                {
                    return NotFound($"No computers found for user with ID {userId}.");
                }
                return Ok(_mapper.Map<IEnumerable<ComputerOnServiceDto>>(computersOnService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerOnServiceDto>> GetComputerOnService(int id)
        {
            try
            {
                var computerOnService = await _computerOnServiceCrudService.GetEntityByIdAsync(id);
                if (computerOnService == null)
                {
                    return NotFound($"Computer on service with ID {id} not found.");
                }
                return Ok(_mapper.Map<ComputerOnServiceDto>(computerOnService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerOnService(int id, ComputerOnServiceUpdateDto computerOnServiceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var ucs = new UsersCrudService(_computerOnServiceCrudService.Context);
                var owner = await ucs.GetEntityByIdAsync(computerOnServiceUpdateDto.UserId);
                
                if (owner == null)
                {
                    return NotFound($"Owner with ID {id} not found.");
                }
                
                bool result = await _computerOnServiceCrudService.UpdateEntityAsync(id, computerOnServiceUpdateDto);

                if (!result)
                {
                    return NotFound($"Computer on service with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchComputerOnService(int id, ComputerOnServicePatchDto computerOnServicePatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (computerOnServicePatchDto.UserId != null)
                {
                    var ucs = new UsersCrudService(_computerOnServiceCrudService.Context);
                    var owner = await ucs.GetEntityByIdAsync((int)computerOnServicePatchDto.UserId);

                    if (owner == null)
                    {
                        return NotFound($"Owner with ID {(int)computerOnServicePatchDto.UserId} not found.");
                    }
                }

                bool result = await _computerOnServiceCrudService.PatchEntityAsync(id, computerOnServicePatchDto);

                if (!result)
                {
                    return NotFound($"Computer on service with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ComputerOnServiceDto>> PostComputerOnService(
            ComputerOnServiceCreateDto computerOnServiceCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var ucs = new UsersCrudService(_computerOnServiceCrudService.Context);
                var owner = await ucs.GetEntityByIdAsync(computerOnServiceCreateDto.UserId);
                
                if (owner == null)
                {
                    return NotFound($"Owner with ID {computerOnServiceCreateDto.UserId} not found.");
                }
                
                var createdComputerOnService = _mapper.Map<ComputerOnService>(computerOnServiceCreateDto);
                bool result = await _computerOnServiceCrudService.CreateEntityAsync(createdComputerOnService);

                if (!result)
                {
                    return BadRequest("Failed to create computer on service.");
                }

                return CreatedAtAction(nameof(GetComputerOnService),
                    new { id = createdComputerOnService.ComputerOnServiceId },
                    _mapper.Map<ComputerOnServiceDto>(createdComputerOnService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerOnService(int id)
        {
            try
            {
                bool result = await _computerOnServiceCrudService.DeleteEntityAsync(id);
                if (!result)
                {
                    return NotFound($"Computer on service with ID {id} not found.");
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