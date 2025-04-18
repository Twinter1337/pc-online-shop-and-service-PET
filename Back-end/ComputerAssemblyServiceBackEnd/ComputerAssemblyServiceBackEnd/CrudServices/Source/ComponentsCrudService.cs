using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ComponentsCrudService : CrudService<Component>
{
    public ComponentsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Component>> GetFilteredComponentsAsync(ComponentFilter filter)
    {
        var query = Context.Components.AsQueryable();

        if (filter.Category.HasValue)
        {
            query = query.Where(c => c.Category == filter.Category);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(c => c.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(c => c.Price <= filter.MaxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer.Contains(filter.Manufacturer));
        }

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            query = query.Where(c => c.Model.Contains(filter.Model));
        }

        return await query
            .Include(c => c.PatternComponents)
            .ThenInclude(pc => pc.Pattern)
            .Include(c => c.Products)
            .ToListAsync();
    }
}