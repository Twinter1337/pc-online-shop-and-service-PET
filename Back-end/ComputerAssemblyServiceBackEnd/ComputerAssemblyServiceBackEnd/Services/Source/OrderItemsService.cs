using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrderItemsService: IOrderItemsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public OrderItemsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateOrderItemAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetAllOrderItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetFilteredOrderItemsAsync(OrderItemFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetOrderItemByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOrderItemAsync(int id, OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderItemAsync(int id)
    {
        throw new NotImplementedException();
    }
}