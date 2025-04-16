using System;
using System.Collections.Generic;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class payments
{
    public int payment_id { get; set; }

    public decimal amount { get; set; }

    public int order_id { get; set; }
    
    public Payment_status status { get; set; }
    
    public Payment_method method { get; set; }

    public virtual orders order { get; set; } = null!;
}
