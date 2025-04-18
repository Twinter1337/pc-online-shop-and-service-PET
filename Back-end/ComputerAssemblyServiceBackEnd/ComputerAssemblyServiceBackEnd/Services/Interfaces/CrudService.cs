using ComputerAssemblyServiceBackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public abstract class CrudService<T> : ICrudService<T> where T : class 
{
    protected CrudService(AppDbContext context)
    {
        Context = context;
    }

    public AppDbContext Context { get; protected set; }

    public async Task<bool> CreateEntityAsync(T entity)
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

    public async Task<List<T>> GetAllEntitiesAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityByIdAsync(int id) 
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<bool> UpdateEntityAsync(int id, T entity) 
    {
        var dbSet = Context.Set<T>();
        
        var existingEntity = await dbSet.FindAsync(id);
        if (existingEntity == null)
            return false;
        
        Context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteEntityAsync(int id) 
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
