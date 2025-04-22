using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderDtos;

public class OrderCreateDto
{
    public int? ClientId { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    [Required]
    public DateOnly CreatedAt { get; set; }
    [Required]
    public OrderStatus Status { get; set; }
}