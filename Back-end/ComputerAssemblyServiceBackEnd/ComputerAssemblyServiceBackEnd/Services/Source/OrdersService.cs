using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrdersService: IOrdersService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public OrdersService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetFilteredOrdersAsync(OrderFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOrderAsync(int id, Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderAsync(int id)
    {
        throw new NotImplementedException();
    }
}