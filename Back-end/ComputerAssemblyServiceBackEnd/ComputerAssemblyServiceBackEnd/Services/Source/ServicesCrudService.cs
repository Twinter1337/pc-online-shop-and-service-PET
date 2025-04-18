using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ServicesCrudService: CrudService<Service>
{
    public ServicesCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<Service> GetServiceByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}