using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class ComputerOnServicePatchDto
{
    public int? UserId { get; set; }
    public string? ProblemDescription { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
    public ServiceStatus? Status { get; set; }
}