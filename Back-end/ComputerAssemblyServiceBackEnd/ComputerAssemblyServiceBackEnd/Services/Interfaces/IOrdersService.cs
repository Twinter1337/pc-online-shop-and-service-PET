using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IOrdersService: IService
{
    Task<bool> CreateOrderAsync(Order order);
    
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetFilteredOrdersAsync(OrderFilter filter);
    Task<Order> GetOrderByIdAsync(int id);
    
    Task<bool> UpdateOrderAsync(int id, Order order);
    Task<bool> DeleteOrderAsync(int id);
}