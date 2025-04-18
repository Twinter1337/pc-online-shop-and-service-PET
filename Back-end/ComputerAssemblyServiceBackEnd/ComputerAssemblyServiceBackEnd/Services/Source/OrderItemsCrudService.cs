using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrderItemsCrudService: CrudService<OrderItem>
{
    public OrderItemsCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<OrderItem>> GetFilteredOrderItemsAsync(OrderItemFilter filter)
    {
        throw new NotImplementedException();
    }
}