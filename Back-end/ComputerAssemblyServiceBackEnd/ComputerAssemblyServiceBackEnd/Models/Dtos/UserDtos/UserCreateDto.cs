using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.UserDtos;

public class UserCreateDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    [Required]
    public UserRole Role { get; set; }
}