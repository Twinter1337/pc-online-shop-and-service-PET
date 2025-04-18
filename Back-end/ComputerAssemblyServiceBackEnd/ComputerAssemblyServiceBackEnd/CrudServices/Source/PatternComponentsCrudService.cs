using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class PatternComponentsCrudService : CrudService<PatternComponent>
{
    public PatternComponentsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<PatternComponent>> GetPatternComponentsByPrebuildPatternIdAsync(int prebuildPatternId)
    {
        return await Context.PatternComponents.Where(pc => pc.Pattern.SerialNumber == prebuildPatternId).ToListAsync();
    }
}