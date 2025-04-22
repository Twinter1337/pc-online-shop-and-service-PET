namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class OrderServicePatchDto
{
    public int? OrderId { get; set; }
    public int? ServiceId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
}