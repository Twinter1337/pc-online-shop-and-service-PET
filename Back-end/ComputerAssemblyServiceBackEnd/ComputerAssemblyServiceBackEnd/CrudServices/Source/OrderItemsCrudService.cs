using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class OrderItemsCrudService: CrudService<OrderItem>
{
    public OrderItemsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<OrderItem>> GetFilteredOrderItemsAsync(OrderItemFilter filter)
    {
        var query = Context.OrderItems.AsQueryable();

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= filter.MaxPrice.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= filter.MinPrice.Value);
        }

        if (filter.OrderId.HasValue)
        {
            query = query.Where(x => x.OrderId == filter.OrderId);
        }
        
        return await query.Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .Include(oi => oi.Product)
            .ThenInclude(p => p.Computer)
            .ToListAsync();
    }

    public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await Context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
    }
}