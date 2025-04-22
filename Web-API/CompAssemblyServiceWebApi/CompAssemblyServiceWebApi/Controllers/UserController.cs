using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.UserDtos;

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
            try
            {
                var users = await _userCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching users: {ex.Message}");
            }
        }

        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            try
            {
                var usersCrudService = _userCrudService as UsersCrudService;
                if (usersCrudService == null)
                    return StatusCode(500, "User service logic not available");

                var user = await usersCrudService.GetUserByEmailAsync(email);
                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching user by email: {ex.Message}");
            }
        }

        [HttpGet("by-phone-number/{phoneNumber}")]
        public async Task<ActionResult<UserDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var usersCrudService = _userCrudService as UsersCrudService;
                if (usersCrudService == null)
                    return StatusCode(500, "User service logic not available");

                var user = await usersCrudService.GetUserByPhoneNumberAsync(phoneNumber);
                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching user by phone number: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _userCrudService.GetEntityByIdAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching user: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _userCrudService.UpdateEntityAsync(id, userUpdateDto);
                if (!result)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating user: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, UserPatchDto userPatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var result = await _userCrudService.PatchEntityAsync(id, userPatchDto);
                if (!result)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var createdUser = _mapper.Map<User>(userCreateDto);
                var result = await _userCrudService.CreateEntityAsync(createdUser);

                if (!result)
                    return BadRequest("Failed to create user");

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId },
                    _mapper.Map<UserDto>(createdUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting user: {ex.Message}");
            }
        }
    }
}