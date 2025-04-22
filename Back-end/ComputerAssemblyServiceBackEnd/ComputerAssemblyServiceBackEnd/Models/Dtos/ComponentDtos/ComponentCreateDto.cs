namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;

public class ComponentCreateDto
{
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int QuantityOnStock { get; set; }
    public Dictionary<string, object>? Specs { get; set; }
}