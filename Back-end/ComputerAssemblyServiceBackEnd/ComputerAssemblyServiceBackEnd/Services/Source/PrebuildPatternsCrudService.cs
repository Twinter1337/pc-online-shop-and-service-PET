using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PrebuildPatternsCrudService: CrudService<PrebuildPattern>
{
    public PrebuildPatternsCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<PrebuildPattern>> GetFilteredPrebuildPatternsAsync(PrebuildPatternFilter filter)
    {
        throw new NotImplementedException();
    }
}