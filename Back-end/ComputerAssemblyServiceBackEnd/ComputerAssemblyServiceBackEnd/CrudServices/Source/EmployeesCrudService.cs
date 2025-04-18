using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class EmployeesCrudService : CrudService<Employee>
{
    public EmployeesCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Employee>> GetFilteredEmployeeAsync(EmployeeFilter filter)
    {
        var query = Context.Employees.AsQueryable();

        if (filter.Position.HasValue)
        {
            query = query.Where(e => e.Position == filter.Position);
        }

        if (filter.MaxSalary.HasValue)
        {
            query = query.Where(e => e.Salary <= filter.MaxSalary);
        }

        if (filter.MinSalary.HasValue)
        {
            query = query.Where(e => e.Salary >= filter.MinSalary);
        }

        if (filter.MinHireDate.HasValue)
        {
            query = query.Where(e => e.HireDate >= filter.MinHireDate);
        }

        if (filter.MaxHireDate.HasValue)
        {
            query = query.Where(e => e.HireDate <= filter.MaxHireDate);
        }

        return await query
            .Include(e => e.User)
            .Include(e => e.PositionNavigation)
            .Include(e => e.ComputersOnService)
            .Include(e => e.OrderServices)
            .ToListAsync();
    }

    public async Task<User> GetEmployeeUserByIdAsync(int employeeId)
    {
        var epUser = await Context.Employees.Include(employee => employee.User)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        return epUser?.User;
    }

    public async Task<Employee> GetEmployeeByBankAccountAsync(string bankAccount)
    {
        return await Context.Employees.FirstOrDefaultAsync(e => e.BankAccount == bankAccount) ??
               throw new
                   InvalidOperationException();
    }
}