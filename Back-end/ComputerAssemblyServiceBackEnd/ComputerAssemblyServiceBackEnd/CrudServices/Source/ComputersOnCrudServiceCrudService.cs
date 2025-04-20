using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ComputersOnCrudServiceCrudService : CrudService<ComputerOnService>
{
    public ComputersOnCrudServiceCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<ComputerOnService>> GetFilteredComputersOnServiceAsync(ComputerOnServiceFilter filter)
    {
        var query = Context.ComputersOnService.AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(c => c.Status == filter.Status.Value);
        }

        if (filter.ResponsibleEmployeeId.HasValue)
        {
            query = query.Where(c => c.ResponsibleEmployeeId == filter.ResponsibleEmployeeId);
        }

        return query.Include(cs => cs.User)
            .Include(cs => cs.ResponsibleEmployee)
            .ThenInclude(e => e.User)
            .ToListAsync();
        ;
    }

    public async Task<List<ComputerOnService>> GetComputersOnServiceByUserIdAsync(int userId)
    {
        return await Context.ComputersOnService.Where(c => c.UserId == userId).ToListAsync() ??
               throw new InvalidOperationException();
    }
}