using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IOrderItemsService: IService
{
    Task<bool> CreateOrderItemAsync(OrderItem orderItem);
    
    Task<List<OrderItem>> GetAllOrderItemsAsync();
    Task<List<OrderItem>> GetFilteredOrderItemsAsync(OrderItemFilter filter);
    Task<OrderItem> GetOrderItemByIdAsync(int id);
    
    Task<bool> UpdateOrderItemAsync(int id, OrderItem orderItem);
    Task<bool> DeleteOrderItemAsync(int id);
}