using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class Product
{
    public int Sku { get; set; }

    public int ComputerId { get; set; }
    
    public ProductType Category { get; set; }
    
    public string ImgUrl { get; set; } = null!;

    public virtual PrebuildPattern? Computer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
