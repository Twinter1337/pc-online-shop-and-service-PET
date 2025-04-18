using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PatternComponentsCrudService: CrudService<PatternComponent>
{
    public PatternComponentsCrudService(AppDbContext context) : base(context)
    {
    }
    
    public Task<List<PatternComponent>> GetPatternComponentsByPrebuildPatternIdAsync(int prebuildPatternId)
    {
        throw new NotImplementedException();
    }
}