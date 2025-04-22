using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;

public abstract class CrudService<T> : ICrudService<T> where T : class 
{
    protected CrudService(AppDbContext context)
    {
        Context = context;
    }

    public AppDbContext Context { get; protected set; }

    public virtual async Task<bool> CreateEntityAsync(T entity)
    {
        var dbSet = Context.Set<T>();
        
        var entry = Context.Entry(entity);
        var key = entry.Metadata.FindPrimaryKey();

        if (key != null)
        {
            var keyValues = key.Properties
                .Select(p => entry.Property(p.Name).CurrentValue)
                .ToArray();
            
            var existingEntity = await dbSet.FindAsync(keyValues);
            if (existingEntity != null)
                return false;
        }
        
        await dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<List<T>> GetAllEntitiesAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetEntityByIdAsync(int id) 
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public virtual async Task<bool> UpdateEntityAsync<TDto>(int id, TDto updateDto)
    {
        var dbSet = Context.Set<T>();

        var existingEntity = await dbSet.FindAsync(id);
        if (existingEntity == null)
            return false;

        var updateProperties = typeof(TDto).GetProperties();

        foreach (var property in updateProperties)
        {
            var entityProperty = typeof(T).GetProperty(property.Name);
            if (entityProperty != null && entityProperty.CanWrite)
            {
                var value = property.GetValue(updateDto);

                if (entityProperty.PropertyType.IsEnum && value != null)
                {
                    try
                    {
                        var enumValue = Enum.Parse(entityProperty.PropertyType, value.ToString());
                        entityProperty.SetValue(existingEntity, enumValue);
                    }
                    catch (ArgumentException)
                    {
                        return false;
                    }
                }
                else
                {
                    if (value != null)
                    {
                        entityProperty.SetValue(existingEntity, value);
                    }
                }
            }
        }

        Context.Entry(existingEntity).CurrentValues.SetValues(existingEntity);

        await Context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> PatchEntityAsync<TPatchDto>(int id, TPatchDto patchDto)
    {
        var dbSet = Context.Set<T>();
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;

        var dtoProperties = typeof(TPatchDto).GetProperties();
        var entityProperties = typeof(T).GetProperties();

        foreach (var dtoProp in dtoProperties)
        {
            var newValue = dtoProp.GetValue(patchDto);
            if (newValue == null)
                continue;

            var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
            if (entityProp != null && entityProp.CanWrite)
            {
                entityProp.SetValue(entity, newValue);
            }
        }

        await Context.SaveChangesAsync();
        return true;
    }
    
    public virtual async Task<bool> DeleteEntityAsync(int id) 
    {
        var dbSet = Context.Set<T>();
        
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;
        
        dbSet.Remove(entity);
        await Context.SaveChangesAsync();
    
        return true;
    }
}
