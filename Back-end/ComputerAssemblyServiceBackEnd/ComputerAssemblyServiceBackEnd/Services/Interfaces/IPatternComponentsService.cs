using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IPatternComponentsService: IService
{
    Task<bool> CreatePatternComponentAsync(PatternComponent patternComponent);

    Task<List<PatternComponent>> GetAllPatternComponentsAsync();
    Task<List<PatternComponent>> GetPatternComponentsByPrebuildPatternIdAsync(int prebuildPatternId);
    Task<PatternComponent> GetPatternComponentByIdAsync(int id);
    
    Task<bool> UpdatePatternComponentAsync(int id, PatternComponent patternComponent);
    Task<bool> DeletePatternComponentAsync(int id);
}