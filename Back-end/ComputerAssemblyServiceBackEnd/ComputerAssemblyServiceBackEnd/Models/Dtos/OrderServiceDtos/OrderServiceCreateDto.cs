using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OrderServiceDtos;

public class OrderServiceCreateDto
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
}