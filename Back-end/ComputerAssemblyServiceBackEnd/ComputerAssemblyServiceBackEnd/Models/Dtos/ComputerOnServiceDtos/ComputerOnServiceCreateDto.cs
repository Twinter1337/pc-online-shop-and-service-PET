using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ComputerOnServiceDtos;

public class ComputerOnServiceCreateDto
{
    public int UserId { get; set; }
    public string ProblemDescription { get; set; } = null!;
    public int? ResponsibleEmployeeId { get; set; }
    public ServiceStatus Status { get; set; }
}