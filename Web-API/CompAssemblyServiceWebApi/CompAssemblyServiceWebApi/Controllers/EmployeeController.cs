using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeeDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.UserDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICrudService<Employee> _employeeCrudService;
        private readonly IMapper _mapper;

        public EmployeeController(ICrudService<Employee> employeeCrudService, IMapper mapper)
        {
            _employeeCrudService = employeeCrudService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            try
            {
                List<Employee> employees = await _employeeCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetFilteredEmployees(
            [FromQuery] EmployeeFilter filter)
        {
            try
            {
                EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
                List<Employee> employees = await employeeCrudService.GetFilteredEmployeeAsync(filter);
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeCrudService.GetEntityByIdAsync(id);

                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("by-user-id/{userId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByUserId(int userId)
        {
            try
            {
                var ecs = _employeeCrudService as EmployeesCrudService;
                var employee = await ecs.GetEmployeeByUserId(userId);

                if (employee == null)
                {
                    return NotFound($"Employee with user ID {userId} not found.");
                }

                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-user/{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetUserByEmployeeId(int employeeId)
        {
            try
            {
                EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
                var employeeUser = await employeeCrudService.GetEmployeeUserByIdAsync(employeeId);

                if (employeeUser == null)
                {
                    return NotFound($"User with Employee ID {employeeId} not found.");
                }

                return Ok(_mapper.Map<UserDto>(employeeUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-bank-account/{bankAccount}")]
        public async Task<ActionResult<EmployeeDto>> GetUserByEmployeeId(string bankAccount)
        {
            try
            {
                EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
                var employee = await employeeCrudService.GetEmployeeByBankAccountAsync(bankAccount);

                if (employee == null)
                {
                    return NotFound($"Employee with bank account {bankAccount} not found.");
                }

                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var ucs = new UsersCrudService(_employeeCrudService.Context);
                var employeeProfile = await ucs.GetEntityByIdAsync(employeeUpdateDto.UserId);

                if (employeeProfile == null)
                {
                    return NotFound($"User with ID {employeeUpdateDto.UserId} not found.");
                }
                
                bool result = await _employeeCrudService.UpdateEntityAsync(id, employeeUpdateDto);

                if (!result)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(int id, EmployeePatchDto employeePatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (employeePatchDto.UserId != null)
                {
                    var ucs = new UsersCrudService(_employeeCrudService.Context);
                    var employeeProfile = await ucs.GetEntityByIdAsync((int)employeePatchDto.UserId);

                    if (employeeProfile == null)
                    {
                        return NotFound($"User with ID {employeePatchDto.UserId} not found.");
                    }
                }

                bool result = await _employeeCrudService.PatchEntityAsync(id, employeePatchDto);

                if (!result)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var ucs = new UsersCrudService(_employeeCrudService.Context);
                var employeeProfile = await ucs.GetEntityByIdAsync(employeeCreateDto.UserId);

                if (employeeProfile == null)
                {
                    return NotFound($"User with ID {employeeCreateDto.UserId} not found.");
                }

                var createdEmployee = _mapper.Map<Employee>(employeeCreateDto);
                bool result = await _employeeCrudService.CreateEntityAsync(createdEmployee);

                if (!result)
                {
                    return BadRequest("Failed to create employee.");
                }

                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId },
                    _mapper.Map<EmployeeDto>(createdEmployee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool result = await _employeeCrudService.DeleteEntityAsync(id);

                if (!result)
                {
                    return NotFound($"Employee with ID {id} not found.");
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