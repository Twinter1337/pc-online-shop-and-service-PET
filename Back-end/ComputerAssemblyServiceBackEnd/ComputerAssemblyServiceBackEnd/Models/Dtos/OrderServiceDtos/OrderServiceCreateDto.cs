namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderServiceDtos;

public class OrderServiceCreateDto
{
    public int OrderId { get; set; }
    public int ServiceId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
}