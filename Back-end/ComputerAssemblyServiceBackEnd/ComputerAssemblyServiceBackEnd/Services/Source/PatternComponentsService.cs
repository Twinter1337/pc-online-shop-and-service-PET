using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PatternComponentsService: IPatternComponentsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public PatternComponentsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreatePatternComponentAsync(PatternComponent patternComponent)
    {
        throw new NotImplementedException();
    }

    public Task<List<PatternComponent>> GetAllPatternComponentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<PatternComponent>> GetPatternComponentsByPrebuildPatternIdAsync(int prebuildPatternId)
    {
        throw new NotImplementedException();
    }

    public Task<PatternComponent> GetPatternComponentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePatternComponentAsync(int id, PatternComponent patternComponent)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePatternComponentAsync(int id)
    {
        throw new NotImplementedException();
    }
}