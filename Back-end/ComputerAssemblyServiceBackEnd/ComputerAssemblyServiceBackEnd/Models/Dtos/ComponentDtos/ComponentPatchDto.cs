using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class ComponentPatchDto
{
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? QuantityOnStock { get; set; }
    public decimal? Price { get; set; }
    public ComponentType? Category { get; set; }
    public Dictionary<string, object>? Characteristics { get; set; }
}