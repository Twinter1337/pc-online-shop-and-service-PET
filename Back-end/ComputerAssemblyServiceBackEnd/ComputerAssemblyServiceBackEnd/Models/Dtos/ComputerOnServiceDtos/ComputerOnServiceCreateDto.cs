using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ComputerOnServiceDtos;

public class ComputerOnServiceCreateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string ProblemDescription { get; set; } = null!;
    public int? ResponsibleEmployeeId { get; set; }
    [Required]
    public ServiceStatus Status { get; set; }
}