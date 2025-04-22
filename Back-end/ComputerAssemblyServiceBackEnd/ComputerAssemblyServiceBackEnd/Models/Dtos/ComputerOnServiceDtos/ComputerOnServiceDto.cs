using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ComputerOnServiceDtos;

public class ComputerOnServiceDto
{
    public int ComputerOnServiceId { get; set; }
    public int UserId { get; set; }
    public string ProblemDescription { get; set; } = null!;
    public int? ResponsibleEmployeeId { get; set; }
    public ServiceStatus Status { get; set; }
}