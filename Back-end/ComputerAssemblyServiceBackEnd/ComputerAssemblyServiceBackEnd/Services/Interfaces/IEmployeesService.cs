using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IEmployeesService : IService
{
    Task<bool> CreateEmployeeAsync(Employee employee);

    Task<List<Employee>> GetAllEmployeesAsync();
    Task<List<Employee>> GetFilteredEmployeeAsync(EmployeeFilter filter);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<User> GetEmployeeUserByIdAsync(int employeeId);
    Task<Employee> GetEmployeeByBankAccountAsync(string bankAccount);

    Task<bool> UpdateEmployeeAsync(int id, Employee employee);
    Task<bool> DeleteEmployeeAsync(int id);
}