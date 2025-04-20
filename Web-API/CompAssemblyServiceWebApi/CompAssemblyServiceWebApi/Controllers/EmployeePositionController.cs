using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

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
            List<EmployeePosition> employeePosition = await _employeePositionCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeePositionDto>>(employeePosition));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePositionDto>> GetEmployeePosition(int id)
        {
            var employeePosition = await _employeePositionCrudService.GetEntityByIdAsync(id);

            if (employeePosition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeePositionDto>(employeePosition));
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<EmployeePositionDto>> GetEmployeePositionByName(string name)
        {
            EmployeePositionsCrudService employeePositionsCrudService =
                (_employeePositionCrudService as EmployeePositionsCrudService)!;
            var employeePosition = await employeePositionsCrudService.GetEmployeePositionByNameAsync(name);

            if (employeePosition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeePositionDto>(employeePosition));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePosition(int id, EmployeePositionDto employeePositionDto)
        {
            if (id != employeePositionDto.PositionId)
            {
                return BadRequest();
            }

            bool result = await _employeePositionCrudService.UpdateEntityAsync(id,
                _mapper.Map<EmployeePosition>(employeePositionDto));

            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeePositionDto>> PostEmployeePosition(EmployeePositionDto employeePositionDto)
        {
            var createdEmployeePosition = _mapper.Map<EmployeePosition>(employeePositionDto);
            bool result = await _employeePositionCrudService.CreateEntityAsync(createdEmployeePosition);
            
            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetEmployeePosition), new { id = createdEmployeePosition.PositionId },
                _mapper.Map<EmployeeDto>(createdEmployeePosition));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePosition(int id)
        {
            bool result = await _employeePositionCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}