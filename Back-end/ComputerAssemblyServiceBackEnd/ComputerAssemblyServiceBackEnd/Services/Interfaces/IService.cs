using ComputerAssemblyServiceBackEnd.Data;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IService
{
    AppDbContext Context { get; }
}
