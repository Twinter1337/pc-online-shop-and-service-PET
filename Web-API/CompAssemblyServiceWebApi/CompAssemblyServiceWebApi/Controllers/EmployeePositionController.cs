using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePositionController : ControllerBase
    {
        private readonly ICrudService<EmployeePosition> _employeePositionCrudService;
        private readonly IMapper _mapper;

        public EmployeePositionController(ICrudService<EmployeePosition> employeePositionCrudService, IMapper mapper)
        {
            _employeePositionCrudService = employeePositionCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePositionDto>>> GetEmployeePositions()
        {
            try
            {
                var employeePosition = await _employeePositionCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<EmployeePositionDto>>(employeePosition));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePositionDto>> GetEmployeePosition(int id)
        {
            try
            {
                var employeePosition = await _employeePositionCrudService.GetEntityByIdAsync(id);

                if (employeePosition == null)
                {
                    return NotFound($"Position with ID {id} not found.");
                }

                return Ok(_mapper.Map<EmployeePositionDto>(employeePosition));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<EmployeePositionDto>> GetEmployeePositionByName(string name)
        {
            try
            {
                var employeePositionsCrudService = (_employeePositionCrudService as EmployeePositionsCrudService)!;
                var employeePosition = await employeePositionsCrudService.GetEmployeePositionByNameAsync(name);

                if (employeePosition == null)
                {
                    return NotFound($"Position with name '{name}' not found.");
                }

                return Ok(_mapper.Map<EmployeePositionDto>(employeePosition));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePosition(int id, EmployeePositionUpdateDto employeePositionUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                bool result = await _employeePositionCrudService.UpdateEntityAsync(id, employeePositionUpdateDto);

                if (!result)
                {
                    return NotFound($"Position with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployeePosition(int id, EmployeePositionPatchDto employeePositionPatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                bool result = await _employeePositionCrudService.PatchEntityAsync(id, employeePositionPatchDto);

                if (!result)
                {
                    return NotFound($"Position with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeePositionDto>> PostEmployeePosition(EmployeePositionCreateDto employeePositionCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var createdEmployeePosition = _mapper.Map<EmployeePosition>(employeePositionCreateDto);
                bool result = await _employeePositionCrudService.CreateEntityAsync(createdEmployeePosition);

                if (!result)
                {
                    return BadRequest("Failed to create new employee position.");
                }

                return CreatedAtAction(nameof(GetEmployeePosition),
                    new { id = createdEmployeePosition.PositionId },
                    _mapper.Map<EmployeePositionDto>(createdEmployeePosition));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePosition(int id)
        {
            try
            {
                bool result = await _employeePositionCrudService.DeleteEntityAsync(id);

                if (!result)
                {
                    return NotFound($"Position with ID {id} not found.");
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