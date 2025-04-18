using ComputerAssemblyServiceBackEnd.Data;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;

public interface ICrudService<T> where T : class
{
    public AppDbContext Context { get; }
    Task<bool> CreateEntityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync();
    Task<T?> GetEntityByIdAsync(int id);
    Task<bool> UpdateEntityAsync(int id, T entity);
    Task<bool> DeleteEntityAsync(int id);
}