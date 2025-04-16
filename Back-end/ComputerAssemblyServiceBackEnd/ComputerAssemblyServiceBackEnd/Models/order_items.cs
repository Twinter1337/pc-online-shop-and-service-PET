using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class order_items
{
    public int item_id { get; set; }

    public int order_id { get; set; }

    public int product_id { get; set; }

    public int quantity { get; set; }

    public decimal? price { get; set; }

    public virtual orders order { get; set; } = null!;

    public virtual products product { get; set; } = null!;
}
