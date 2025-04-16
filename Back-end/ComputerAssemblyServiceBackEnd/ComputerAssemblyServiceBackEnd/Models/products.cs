using System;
using System.Collections.Generic;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class products
{
    public int sku { get; set; }

    public int? computer_id { get; set; }

    public int? component_id { get; set; }
    
    public Product_type category { get; set; }

    public virtual components? component { get; set; }

    public virtual prebuild_patterns? computer { get; set; }

    public virtual ICollection<order_items> order_items { get; set; } = new List<order_items>();
}
