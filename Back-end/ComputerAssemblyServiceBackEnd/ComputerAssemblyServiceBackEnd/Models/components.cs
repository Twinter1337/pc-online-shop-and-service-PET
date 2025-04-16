using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class components
{
    public int component_id { get; set; }

    public string manufacturer { get; set; } = null!;

    public string model { get; set; } = null!;

    public int quantity_on_stock { get; set; }

    public decimal price { get; set; }
    
    public Component_type category { get; set; }

    public virtual ICollection<pattern_components> pattern_components { get; set; } = new List<pattern_components>();

    public virtual products? products { get; set; }
}
