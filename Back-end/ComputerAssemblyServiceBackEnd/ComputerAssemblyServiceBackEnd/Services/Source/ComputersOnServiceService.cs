using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ComputersOnServiceService: IComputersOnServiceService
{
    private AppDbContext _context;
    public AppDbContext Context { get; }

    public ComputersOnServiceService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateComputerOnServiceAsync(ComputerOnService computerOnService)
    {
        throw new NotImplementedException();
    }

    public Task<List<ComputerOnService>> GetAllComputerOnServiceAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<ComputerOnService>> GetFilteredComputersOnServiceAsync(ComputerOnServiceFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<ComputerOnService> GetComputerOnServiceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ComputerOnService> GetComputerOnServiceByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateComputerOnServiceAsync(int id, ComputerOnService computerOnService)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteComputerOnServiceAsync(int id)
    {
        throw new NotImplementedException();
    }
}