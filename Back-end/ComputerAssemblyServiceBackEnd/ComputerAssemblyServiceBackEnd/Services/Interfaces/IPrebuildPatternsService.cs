using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IPrebuildPatternsService: IService
{
    Task<bool> CreatePrebuildPattern(PrebuildPattern pattern);
    
    Task<List<PrebuildPattern>> GetAllPrebuildPatternsAsync();
    Task<List<PrebuildPattern>> GetFilteredPrebuildPatternsAsync(PrebuildPatternFilter filter);
    Task<PrebuildPattern> GetPrebuildPatternByIdAsync(int id);
    
    Task<bool> UpdatePrebuildPatternAsync(int id, PrebuildPattern pattern);
    Task<bool> DeletePrebuildPatternAsync(int id);
}