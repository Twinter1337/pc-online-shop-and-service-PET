using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class ComponentFilter
{
    public ComponentType? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
}