using System.Xml.Serialization;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? ClientId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateOnly CreatedAt { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public virtual User? Client { get; set; }

    [XmlIgnore]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [XmlIgnore]
    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    [XmlIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
