using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class employees
{
    public int employee_id { get; set; }

    public int user_id { get; set; }

    public string bank_account { get; set; } = null!;

    public decimal salary { get; set; }

    public int position { get; set; }

    public DateOnly hire_date { get; set; }

    public virtual ICollection<computers_on_service> computers_on_service { get; set; } = new List<computers_on_service>();

    public virtual ICollection<order_services> order_services { get; set; } = new List<order_services>();

    public virtual employee_positions positionNavigation { get; set; } = null!;

    public virtual users user { get; set; } = null!;
}
