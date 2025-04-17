using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ServicesService: IServicesService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public ServicesService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateServiceAsync(Service service)
    {
        throw new NotImplementedException();
    }

    public Task<List<Service>> GetAllServicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Service> GetServiceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Service> GetServiceByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateServiceAsync(int id, Service service)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteServiceAsync(int id)
    {
        throw new NotImplementedException();
    }
}