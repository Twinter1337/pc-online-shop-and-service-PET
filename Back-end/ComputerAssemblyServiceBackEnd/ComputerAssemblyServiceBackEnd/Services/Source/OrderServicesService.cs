using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrderServicesService: IOrderServicesService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public OrderServicesService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateOrderServiceAsync(OrderService orderItem)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderService>> GetAllOrderServicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderService> GetOrderServiceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOrderServiceAsync(int id, OrderService orderService)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderServiceAsync(int id)
    {
        throw new NotImplementedException();
    }
}