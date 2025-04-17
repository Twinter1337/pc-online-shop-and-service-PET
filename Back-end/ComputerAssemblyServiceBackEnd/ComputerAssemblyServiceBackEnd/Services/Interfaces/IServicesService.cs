using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IServicesService: IService
{
    Task<bool> CreateServiceAsync(Service service);
    
    Task<List<Service>> GetAllServicesAsync();
    Task<Service> GetServiceByIdAsync(int id);
    Task<Service> GetServiceByNameAsync(string name);
    
    Task<bool> UpdateServiceAsync(int id, Service service);
    Task<bool> DeleteServiceAsync(int id);
}