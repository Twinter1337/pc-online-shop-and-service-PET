using System;
using System.Collections.Generic;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class orders
{
    public int order_id { get; set; }

    public int? client_id { get; set; }

    public decimal total_amount { get; set; }

    public DateOnly created_at { get; set; }
    
    public Order_status status { get; set; }

    public virtual ICollection<order_items> order_items { get; set; } = new List<order_items>();

    public virtual ICollection<order_services> order_services { get; set; } = new List<order_services>();

    public virtual ICollection<payments> payments { get; set; } = new List<payments>();
}
