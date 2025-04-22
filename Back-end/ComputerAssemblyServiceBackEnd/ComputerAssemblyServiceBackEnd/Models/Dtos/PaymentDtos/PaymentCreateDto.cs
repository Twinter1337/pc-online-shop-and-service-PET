using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PaymentDtos;

public class PaymentCreateDto
{
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public PaymentStatus Status { get; set; }
    [Required]
    public PaymentMethod Method { get; set; }
}