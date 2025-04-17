using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class ComputerOnServiceFilter
{
    public ServiceStatus? Status { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
}