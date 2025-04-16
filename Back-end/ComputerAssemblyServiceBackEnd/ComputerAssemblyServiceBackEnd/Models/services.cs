using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class services
{
    public int service_id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public decimal price { get; set; }

    public virtual ICollection<order_services> order_services { get; set; } = new List<order_services>();
}
