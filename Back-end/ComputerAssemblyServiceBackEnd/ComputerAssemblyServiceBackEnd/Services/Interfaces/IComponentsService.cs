using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IComponentsService : IService
{
    Task<bool> CreateComponentAsync(Component? component);
    
    Task<List<Component>> GetAllComponentsAsync();
    Task<List<Component?>> GetFilteredComponentsAsync(ComponentFilter filter);
    Task<Component?> GetComponentByIdAsync(int id);
    
    Task<bool> UpdateComponentAsync(int id, Component c);
    
    Task<bool> DeleteComponentAsync(int id);
}