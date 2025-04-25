using System.Text.Json.Nodes;
using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class PrebuildPatternsCrudService : CrudService<PrebuildPattern>
{
    public PrebuildPatternsCrudService(AppDbContext context) : base(context)
    {
    }

    public List<ComponentDto> GetComponentsForPattern(PrebuildPattern pattern, IMapper mapper)
    {
        List<ComponentDto> componentDtos = new List<ComponentDto>();
        
        foreach (var component in pattern.PatternComponents)
        {
            componentDtos.Add(mapper.Map<ComponentDto>(component.Component));
        }
        
        return componentDtos;
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