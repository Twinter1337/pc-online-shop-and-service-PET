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
            List<Employee> employees = await _employeeCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetFilteredEmployees(
            [FromQuery] EmployeeFilter filter)
        {
            EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
            List<Employee> employees = await employeeCrudService.GetFilteredEmployeeAsync(filter);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeCrudService.GetEntityByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpGet("get-user/{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetUserByEmployeeId(int employeeId)
        {
            EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
            var employeeUser = await employeeCrudService.GetEmployeeUserByIdAsync(employeeId);

            if (employeeUser == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(employeeUser));
        }

        [HttpGet("by-bank-account/{bankAccount}")]
        public async Task<ActionResult<EmployeeDto>> GetUserByEmployeeId(string bankAccount)
        {
            EmployeesCrudService employeeCrudService = (_employeeCrudService as EmployeesCrudService)!;
            var employee = await employeeCrudService.GetEmployeeByBankAccountAsync(bankAccount);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return BadRequest();
            }

            bool result = await _employeeCrudService.UpdateEntityAsync(id, _mapper.Map<Employee>(employeeDto));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            var createdEmployee = _mapper.Map<Employee>(employeeDto);
            bool result = await _employeeCrudService.CreateEntityAsync(createdEmployee);
            
            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId },
                _mapper.Map<EmployeeDto>(createdEmployee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            bool result = await _employeeCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}