using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class PrebuildPatternsCrudService : CrudService<PrebuildPattern>
{
    public PrebuildPatternsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<PrebuildPattern>> GetFilteredPrebuildPatternsAsync(PrebuildPatternFilter filter)
    {
        var query = Context.PrebuildPatterns.AsQueryable();

        if (filter.Manufacturer is not (null or ""))
        {
            query = query.Where(p => p.Manufacturer.Equals(filter.Manufacturer));
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.BasePrice <= filter.MaxPrice.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.BasePrice >= filter.MinPrice.Value);
        }
        
        return await query.Include(pp => pp.PatternComponents)
            .ThenInclude(pc => pc.Component)
            .Include(pp => pp.Product)
            .ToListAsync(); 
    }
}