namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderItemDtos;

public class OrderItemDto
{
    public int ItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}