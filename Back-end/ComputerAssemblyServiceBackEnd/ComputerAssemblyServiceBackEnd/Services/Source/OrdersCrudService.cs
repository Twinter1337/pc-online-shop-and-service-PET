using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrdersCrudService: CrudService<Order>
{
    public OrdersCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<Order>> GetFilteredOrdersAsync(OrderFilter filter)
    {
        throw new NotImplementedException();
    }
}