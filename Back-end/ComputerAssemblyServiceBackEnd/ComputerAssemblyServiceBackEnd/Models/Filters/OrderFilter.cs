using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class OrderFilter
{
    public int? ClientId { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    
    public DateOnly? MinCreationDate { get; set; }
    
    public DateOnly? MaxCreatioDate { get; set; }
    public OrderStatus? Status { get; set; }
}