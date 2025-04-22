namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderItemDtos;

public class OrderItemUpdateDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? Price { get; set; }
}