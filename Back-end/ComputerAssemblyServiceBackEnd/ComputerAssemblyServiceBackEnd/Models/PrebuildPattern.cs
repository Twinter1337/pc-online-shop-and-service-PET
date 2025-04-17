namespace ComputerAssemblyServiceBackEnd.Models;

public partial class PrebuildPattern
{
    public int SerialNumber { get; set; }

    public string PrebuildName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public virtual ICollection<PatternComponent> PatternComponents { get; set; } = new List<PatternComponent>();

    public virtual Product? Product { get; set; }
}
