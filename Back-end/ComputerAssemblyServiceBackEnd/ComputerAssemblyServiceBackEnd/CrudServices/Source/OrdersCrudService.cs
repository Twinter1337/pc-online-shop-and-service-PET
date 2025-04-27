using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class OrdersCrudService: CrudService<Order>
{
    public OrdersCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetFilteredOrdersAsync(OrderFilter filter)
    {
        var query = Context.Orders.AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(o => o.Status == filter.Status);
        }

        if (filter.ClientId.HasValue || filter.ClientId == null)
        {
            query = query.Where(o => o.ClientId == filter.ClientId);
        }

        if (filter.MaxAmount.HasValue)
        {
            query = query.Where(o => o.TotalAmount <= filter.MaxAmount);
        }

        if (filter.MinAmount.HasValue)
        {
            query = query.Where(o => o.TotalAmount >= filter.MinAmount);
        }
        
        if (filter.MinCreationDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= filter.MinCreationDate);
        }
        
        if (filter.MaxCreatioDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= filter.MaxCreatioDate);
        }
        
        return await query.Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o => o.OrderServices)
            .ThenInclude(os => os.Service)
            .Include(o => o.Payments)
            .ToListAsync();
    }
}