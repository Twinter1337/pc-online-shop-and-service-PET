using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.DbHelper;

public class EfDbHelper: IDbHelper
{
    private readonly AppDbContext _dbContext; 
    public AppDbContext DbContext => _dbContext;

    public EfDbHelper(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<components>> GetAllComponentsAsync()
    {
        return await _dbContext.components
            .Include(c => c.pattern_components)
            .ThenInclude(pc => pc.pattern)
            .Include(c => c.products)
            .ToListAsync();
    }
    
    public async Task<List<prebuild_patterns>> GetAllPrebuildPatternsAsync()
    {
        return await _dbContext.prebuild_patterns
            .Include(pp => pp.pattern_components)
            .ThenInclude(pc => pc.component)
            .Include(pp => pp.products)
            .ToListAsync();
    }

    public async Task<List<computers_on_service>> GetAllComputersOnServiceAsync()
    {
        return await _dbContext.computers_on_service
            .Include(cs => cs.user)
            .Include(cs => cs.responsible_employee)
            .ThenInclude(e => e.user)
            .ToListAsync();
    }

    public async Task<List<employees>> GetAllEmployeesAsync()
    {
        return await _dbContext.employees
            .Include(e => e.user)
            .Include(e => e.positionNavigation)
            .Include(e => e.computers_on_service)
            .Include(e => e.order_services)
            .ToListAsync();
    }

    public async Task<List<order_items>> GetAllOrderItemsAsync()
    {
        return await _dbContext.order_items
            .Include(oi => oi.order)
            .Include(oi => oi.product)
            .ThenInclude(p => p.component)
            .Include(oi => oi.product)
            .ThenInclude(p => p.computer)
            .ToListAsync();
    }

    public async Task<List<order_services>> GetAllOrderServicesAsync()
    {
        return await _dbContext.order_services
            .Include(os => os.order)
            .Include(os => os.service)
            .Include(os => os.responsible_employee)
            .ThenInclude(e => e.user)
            .ToListAsync();
    }
    public async Task<List<pattern_components>> GetAllPatternComponentsAsync()
    {
        return await _dbContext.pattern_components
            .Include(pc => pc.component)
            .Include(pc => pc.pattern)
            .ToListAsync();
    }
    public async Task<List<products>> GetAllProductsAsync()
    {
        return await _dbContext.products
            .Include(p => p.component)
            .Include(p => p.computer)
            .Include(p => p.order_items)
            .ToListAsync();
    }
    
    public async Task<List<orders>> GetAllOrdersAsync()
    {
        return await _dbContext.orders
            .Include(o => o.order_items)
            .ThenInclude(oi => oi.product)
            .Include(o => o.order_services)
            .ThenInclude(os => os.service)
            .Include(o => o.payments)
            .ToListAsync();
    }
    
    public async Task<List<payments>> GetAllPaymentsAsync()
    {
        return await _dbContext.payments
            .Include(p => p.order)
            .ToListAsync();
    }

    public async Task<List<services>> GetAllServicesAsync()
    {
        return await _dbContext.services
            .Include(s => s.order_services)
            .ToListAsync();
    }

    public async Task<List<users>> GetAllUsersAsync()
    {
        return await _dbContext.users
            .Include(u => u.computers_on_service)
            .Include(u => u.employees)
            .ThenInclude(e => e.positionNavigation)
            .ToListAsync();
    }

    public async Task<List<employee_positions>> GetAllEmployeePositionsWithEmployeesAsync()
    {
        return await _dbContext.employee_positions
            .Include(p => p.employees).ToListAsync();
    }
}