namespace ComputerAssemblyServiceBackEnd.Models.Dtos;

public class OrderServiceDto
{
    public int OrderServiceId { get; set; }
    public int OrderId { get; set; }
    public int ServiceId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
}