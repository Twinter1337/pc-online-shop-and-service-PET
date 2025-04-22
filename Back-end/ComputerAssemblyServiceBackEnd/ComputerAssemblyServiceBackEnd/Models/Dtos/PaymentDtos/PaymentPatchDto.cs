using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class PaymentPatchDto
{
    public decimal? Amount { get; set; }
    public int? OrderId { get; set; }
    public PaymentStatus? Status { get; set; }
    public PaymentMethod? Method { get; set; }
}