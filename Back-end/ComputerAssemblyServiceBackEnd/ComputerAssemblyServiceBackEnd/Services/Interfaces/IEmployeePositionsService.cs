using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IEmployeePositionsService : IService
{
    Task<bool> CreateEmployeePositionAsync(EmployeePosition employeePosition);
    
    Task<List<EmployeePosition>> GetAllEmployeePositionsAsync();
    Task<EmployeePosition> GetEmployeePositionByIdAsync(int id);
    Task<EmployeePosition> GetEmployeePositionByNameAsync(string name);
    
    Task<bool> UpdateEmployeePositionsAsync(int id, EmployeePosition employeePosition);
    Task<bool> DeleteEmployeePositionsAsync(int id);
}