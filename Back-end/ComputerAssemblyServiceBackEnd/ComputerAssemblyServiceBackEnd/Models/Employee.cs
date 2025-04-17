using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public string BankAccount { get; set; } = null!;

    public decimal Salary { get; set; }

    public int Position { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual ICollection<ComputerOnService> ComputersOnService { get; set; } = new List<ComputerOnService>();

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual EmployeePosition PositionNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
