using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class EmployeePosition
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public int MaximumNumberOfEmpolyees { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
