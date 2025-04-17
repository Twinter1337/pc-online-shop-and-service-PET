using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class ComputerOnService
{
    public int ComputerOnServiceId { get; set; }

    public int UserId { get; set; }

    public string ProblemDescription { get; set; } = null!;

    public int? ResponsibleEmployeeId { get; set; }
    
    public ServiceStatus Status { get; set; }

    public virtual Employee? ResponsibleEmployee { get; set; }

    public virtual User User { get; set; } = null!;
}
