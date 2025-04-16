using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class order_services
{
    public int order_service_id { get; set; }

    public int order_id { get; set; }

    public int service_id { get; set; }

    public int? responsible_employee_id { get; set; }

    public virtual orders order { get; set; } = null!;

    public virtual employees? responsible_employee { get; set; }

    public virtual services service { get; set; } = null!;
}
