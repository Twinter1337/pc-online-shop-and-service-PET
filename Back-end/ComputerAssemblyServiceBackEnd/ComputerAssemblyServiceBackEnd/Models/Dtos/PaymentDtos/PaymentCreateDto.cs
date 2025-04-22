using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PaymentDtos;

public class PaymentCreateDto
{
    public decimal Amount { get; set; }
    public int OrderId { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
}