using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IComputersOnServiceService : IService
{
    Task<bool> CreateComputerOnServiceAsync(ComputerOnService computerOnService);
    
    Task<List<ComputerOnService>> GetAllComputerOnServiceAsync();
    Task<List<ComputerOnService>> GetFilteredComputersOnServiceAsync(ComputerOnServiceFilter filter);
    Task<ComputerOnService> GetComputerOnServiceByIdAsync(int id);
    Task<ComputerOnService> GetComputerOnServiceByUserIdAsync(int userId);
    
    Task<bool> UpdateComputerOnServiceAsync(int id, ComputerOnService computerOnService);
    Task<bool> DeleteComputerOnServiceAsync(int id);
}