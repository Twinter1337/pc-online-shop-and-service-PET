using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;

public class EmployeePositionCreateDto
{
    [Required]
    public string PositionName { get; set; } = null!;
    [Required]
    public int MaximumNumberOfEmpolyees { get; set; }
}