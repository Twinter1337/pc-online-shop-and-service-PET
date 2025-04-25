using System.Text.Json;
using System.Text.Json.Nodes;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ComponentsCrudService : CrudService<Component>
{
    public ComponentsCrudService(AppDbContext context) : base(context)
    {
    }

    public override async Task<bool> PatchEntityAsync<TPatchDto>(int id, TPatchDto patchDto)
    {
        var dbSet = Context.Set<Component>();
        var existingEntity = await dbSet.FindAsync(id);
        if (existingEntity == null)
            return false;

        var patchProperties = typeof(TPatchDto).GetProperties();

        foreach (var patchProp in patchProperties)
        {
            var entityProp = typeof(Component).GetProperty(patchProp.Name);
            if (entityProp == null || !entityProp.CanWrite)
                continue;

            var value = patchProp.GetValue(patchDto);
            if (value == null)
                continue;

            // Обробка JSON Characteristics
            if (patchProp.Name == nameof(Component.Characteristics))
            {
                if (value is Dictionary<string, object> specsDict)
                {
                    var json = JsonSerializer.Serialize(specsDict);
                    var jsonObject = JsonNode.Parse(json)?.AsObject();
                    if (jsonObject != null)
                    {
                        existingEntity.Characteristics = jsonObject;
                    }
                }
                continue;
            }

            // Обробка enum
            if (entityProp.PropertyType.IsEnum)
            {
                try
                {
                    var enumValue = Enum.Parse(entityProp.PropertyType, value.ToString());
                    entityProp.SetValue(existingEntity, enumValue);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
            else
            {
                entityProp.SetValue(existingEntity, value);
            }
        }

        await Context.SaveChangesAsync();
        return true;
    }
    
    public override async Task<bool> UpdateEntityAsync<TDto>(int id, TDto updateDto)
    {
        var dbSet = Context.Set<Component>();
        var existingEntity = await dbSet.FindAsync(id);
        if (existingEntity == null)
            return false;
    
        var dtoProperties = typeof(ComponentUpdateDto).GetProperties();
        
        Console.WriteLine(dtoProperties.Length);
    
        foreach (var dtoProp in dtoProperties)
        {
            var entityProp = typeof(Component).GetProperty(dtoProp.Name);
            if (entityProp == null || !entityProp.CanWrite)
                continue;
    
            var value = dtoProp.GetValue(updateDto);
            if (value == null)
                continue;
            
            Console.Error.WriteLine(dtoProp.Name + " " + nameof(ComponentUpdateDto.Characteristics));
            
            if (dtoProp.Name == nameof(ComponentUpdateDto.Characteristics) && value is Dictionary<string, object> dictValue)
            {
                var json = JsonSerializer.Serialize(dictValue);
                var jsonObject = JsonNode.Parse(json)?.AsObject();
                Console.Error.WriteLine(jsonObject);
    
                if (jsonObject != null)
                {
                    var entityProperty = typeof(Component).GetProperty("Characteristics");
                    if (entityProperty != null)
                    {
                        entityProperty.SetValue(existingEntity, jsonObject);
                    }
                }
    
                continue;
            }
            
            if (entityProp.PropertyType.IsEnum)
            {
                try
                {
                    var enumValue = Enum.Parse(entityProp.PropertyType, value.ToString());
                    entityProp.SetValue(existingEntity, enumValue);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
            else
            {
                entityProp.SetValue(existingEntity, value);
            }
        }
    
        await Context.SaveChangesAsync();
        return true;
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
            .ToListAsync();
    }
}