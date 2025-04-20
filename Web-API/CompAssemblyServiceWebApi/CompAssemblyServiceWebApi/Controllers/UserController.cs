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
    public class UserController : ControllerBase
    {
        private readonly ICrudService<User> _userCrudService;
        private readonly IMapper _mapper;

        public UserController(ICrudService<User> userCrudService, IMapper mapper)
        {
            _userCrudService = userCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            List<User> users = await _userCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
        
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            UsersCrudService usersCrudService = (_userCrudService as UsersCrudService)!;
            User? user = await usersCrudService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<UserDto>(user));
        }
        
        [HttpGet("by-phone-number/{phoneNumber}")]
        public async Task<ActionResult<UserDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            UsersCrudService usersCrudService = (_userCrudService as UsersCrudService)!;
            User? user = await usersCrudService.GetUserByPhoneNumberAsync(phoneNumber);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userCrudService.GetEntityByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            bool result = await _userCrudService.UpdateEntityAsync(id, _mapper.Map<User>(userDto));

            if (!result)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var createdUser = _mapper.Map<User>(userDto);
            bool result = await _userCrudService.CreateEntityAsync(createdUser);

            if (!result)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId },
                _mapper.Map<UserDto>(createdUser));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool result = await _userCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}