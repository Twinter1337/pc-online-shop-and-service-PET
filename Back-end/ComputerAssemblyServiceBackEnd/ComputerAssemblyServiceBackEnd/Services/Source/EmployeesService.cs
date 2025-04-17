using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class EmployeesService: IEmployeesService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public EmployeesService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateEmployeeAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<List<Employee>> GetAllEmployeesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Employee>> GetFilteredEmployeeAsync(EmployeeFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetEmployeeByIdAsync(int id)
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

    public Task<bool> UpdateEmployeeAsync(int id, Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEmployeeAsync(int id)
    {
        throw new NotImplementedException();
    }
}