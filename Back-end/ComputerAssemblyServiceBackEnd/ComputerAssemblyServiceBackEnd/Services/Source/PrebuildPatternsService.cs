using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PrebuildPatternsService: IPrebuildPatternsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public PrebuildPatternsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreatePrebuildPattern(PrebuildPattern pattern)
    {
        throw new NotImplementedException();
    }

    public Task<List<PrebuildPattern>> GetAllPrebuildPatternsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<PrebuildPattern>> GetFilteredPrebuildPatternsAsync(PrebuildPatternFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<PrebuildPattern> GetPrebuildPatternByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePrebuildPatternAsync(int id, PrebuildPattern pattern)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePrebuildPatternAsync(int id)
    {
        throw new NotImplementedException();
    }
}