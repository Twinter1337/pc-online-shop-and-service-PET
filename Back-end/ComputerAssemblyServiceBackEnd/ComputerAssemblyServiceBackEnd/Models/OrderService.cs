namespace ComputerAssemblyServiceBackEnd.Models;

public partial class OrderService
{
    public int OrderServiceId { get; set; }

    public int OrderId { get; set; }

    public int ServiceId { get; set; }

    public int? ResponsibleEmployeeId { get; set; }
    
    public int? ComputerOnServiceId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Employee? responsibleEmployee { get; set; }

    public virtual Service Service { get; set; } = null!;
}
