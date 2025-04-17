using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class EmployeePositionsService: IEmployeePositionsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public EmployeePositionsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateEmployeePositionAsync(EmployeePosition employeePosition)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmployeePosition>> GetAllEmployeePositionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<EmployeePosition> GetEmployeePositionByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeePosition> GetEmployeePositionByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEmployeePositionsAsync(int id, EmployeePosition employeePosition)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEmployeePositionsAsync(int id)
    {
        throw new NotImplementedException();
    }
}