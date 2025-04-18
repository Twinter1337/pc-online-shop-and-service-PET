using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class EmployeePositionsCrudService : CrudService<EmployeePosition>
{
    public EmployeePositionsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<EmployeePosition> GetEmployeePositionByNameAsync(string name)
    {
        return await Context.EmployeePositions.FirstOrDefaultAsync(ep => ep.PositionName == name) ?? throw new InvalidOperationException();
    }
}