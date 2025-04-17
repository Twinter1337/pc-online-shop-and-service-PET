using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ComponentsService : IComponentsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public ComponentsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateComponentAsync(Component? component)
    {
        if (await _context.Components.FindAsync(component.ComponentId) != null) return false;

        await _context.Components.AddAsync(component);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Component>> GetAllComponentsAsync()
    {
        return await _context.Components
            .Include(c => c.PatternComponents)
            .ThenInclude(pc => pc.Pattern)
            .Include(c => c.Products)
            .ToListAsync();
    }

    public async Task<List<Component?>> GetFilteredComponentsAsync(ComponentFilter filter)
    {
        var query = _context.Components.AsQueryable();

        if (filter.Category.HasValue)
        {
            query = query.Where(c => c != null && c.Category == filter.Category);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(c => c != null && c.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(c => c != null && c.Price <= filter.MaxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            query = query.Where(c => c != null && c.Manufacturer.Contains(filter.Manufacturer));
        }

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            query = query.Where(c => c != null && c.Model.Contains(filter.Model));
        }

        return await query
            .Include(c => c!.PatternComponents)
            .ThenInclude(pc => pc.Pattern)
            .Include(c => c!.Products)
            .ToListAsync();
    }

    public async Task<Component?> GetComponentByIdAsync(int id)
    {
        return await _context.Components.FindAsync(id);
    }

    public async Task<bool> UpdateComponentAsync(int id, Component c)
    {
        var existing = await _context.Components.FindAsync(id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(c);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteComponentAsync(int id)
    {
        var component = await _context.Components.FindAsync(id);
        if (component == null) return false;

        _context.Components.Remove(component);
        await _context.SaveChangesAsync();
        
        return true;
    }
}