using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ServicesCrudService: CrudService<Service>
{
    public ServicesCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<Service?> GetServiceByNameAsync(string name)
    {
        return await Context.Services.FirstOrDefaultAsync(s => s.Name == name) ?? throw new InvalidOperationException();
    }
}