using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderItemDtos;

public class OrderItemCreateDto
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal? Price { get; set; }
}