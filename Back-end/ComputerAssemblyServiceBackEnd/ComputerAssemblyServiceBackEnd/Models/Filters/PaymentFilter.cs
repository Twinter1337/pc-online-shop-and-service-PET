using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class PaymentFilter
{
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public PaymentStatus? Status { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
}