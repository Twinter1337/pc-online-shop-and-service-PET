using System.Text.Json.Nodes;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class Component
{
    public int ComponentId { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int QuantityOnStock { get; set; }

    public decimal Price { get; set; }
    
    public ComponentType Category { get; set; }
    
    public JsonObject? Characteristics { get; set; }

    public virtual ICollection<PatternComponent> PatternComponents { get; set; } = new List<PatternComponent>();

    public virtual Product? Products { get; set; }
}
