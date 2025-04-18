using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class EmployeesCrudService: CrudService<Employee>
{
    public EmployeesCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<Employee>> GetFilteredEmployeeAsync(EmployeeFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetEmployeeUserByIdAsync(int employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetEmployeeByBankAccountAsync(string bankAccount)
    {
        throw new NotImplementedException();
    }
}