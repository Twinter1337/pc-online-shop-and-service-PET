using System;
using System.Collections.Generic;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class computers_on_service
{
    public int computer_on_service_id { get; set; }

    public int user_id { get; set; }

    public string problem_description { get; set; } = null!;

    public int? responsible_employee_id { get; set; }
    
    public Service_status status { get; set; }

    public virtual employees? responsible_employee { get; set; }

    public virtual users user { get; set; } = null!;
}
