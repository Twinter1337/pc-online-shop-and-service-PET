using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IOrderServicesService: IService
{
    Task<bool> CreateOrderServiceAsync(OrderService orderItem);
    
    Task<List<OrderService>> GetAllOrderServicesAsync();
    Task<OrderService> GetOrderServiceByIdAsync(int id);
    
    Task<bool> UpdateOrderServiceAsync(int id, OrderService orderService);
    Task<bool> DeleteOrderServiceAsync(int id);
}