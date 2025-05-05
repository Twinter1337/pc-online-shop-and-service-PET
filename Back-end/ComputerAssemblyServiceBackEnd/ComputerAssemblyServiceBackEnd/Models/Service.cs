using System.Xml.Serialization;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    [XmlIgnore]
    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
