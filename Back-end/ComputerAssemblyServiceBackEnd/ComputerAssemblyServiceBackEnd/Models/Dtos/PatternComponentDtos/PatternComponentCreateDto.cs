using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatternComponentDtos;

public class PatternComponentCreateDto
{
    [Required]
    public int PatternId { get; set; }
    [Required]
    public int ComponentId { get; set; }
    public int? Quantity { get; set; }
}