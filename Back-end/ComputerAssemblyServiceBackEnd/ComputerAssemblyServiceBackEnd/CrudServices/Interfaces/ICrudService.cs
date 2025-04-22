using ComputerAssemblyServiceBackEnd.Data;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;

public interface ICrudService<T> where T : class
{
    public AppDbContext Context { get; }
    Task<bool> CreateEntityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync();
    Task<T?> GetEntityByIdAsync(int id);
    Task<bool> UpdateEntityAsync<TDto>(int id, TDto entity);
    Task<bool> PatchEntityAsync<TPatchDto>(int id, TPatchDto patchDto);
    Task<bool> DeleteEntityAsync(int id);
}