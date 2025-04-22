using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;

public class ComponentCreateDto
{
    [Required]
    public string Manufacturer { get; set; } = null!;
    [Required]
    public string Model { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public int QuantityOnStock { get; set; }
    public Dictionary<string, object>? Specs { get; set; }
}