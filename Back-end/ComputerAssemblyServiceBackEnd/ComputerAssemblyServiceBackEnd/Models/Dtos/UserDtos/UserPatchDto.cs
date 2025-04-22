using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class UserPatchDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Email { get; set; } 
    public string? PhoneNumber { get; set; }
    public UserRole? Role { get; set; }
}