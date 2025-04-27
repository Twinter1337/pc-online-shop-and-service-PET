using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class OrderServicesCrudService: CrudService<OrderService>
{
    public OrderServicesCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<OrderService>> GetOrderServicesByOrderIdAsync(int orderId)
    {
        return await Context.OrderServices.Where(os => os.OrderId == orderId).ToListAsync();
    }
}