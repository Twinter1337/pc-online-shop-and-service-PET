using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderDtos;

public class OrderDto
{
    public int OrderId { get; set; }
    public int? ClientId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateOnly CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
}