using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class OrderPatchDto
{
    public int? ClientId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateOnly? CreatedAt { get; set; }
    public OrderStatus? Status { get; set; }
}