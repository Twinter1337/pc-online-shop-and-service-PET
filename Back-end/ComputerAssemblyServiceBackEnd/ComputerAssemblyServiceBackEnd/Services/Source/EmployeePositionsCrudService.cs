using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class EmployeePositionsCrudService: CrudService<EmployeePosition>
{
    public EmployeePositionsCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<EmployeePosition> GetEmployeePositionByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}