namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderServiceDtos;

public class OrderServiceUpdateDto
{
    public int OrderId { get; set; }
    public int ServiceId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
    public int? ComputerOnServiceId { get; set; }
}