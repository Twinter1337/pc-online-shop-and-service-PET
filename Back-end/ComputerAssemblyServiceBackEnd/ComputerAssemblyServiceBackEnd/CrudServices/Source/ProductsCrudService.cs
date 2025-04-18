using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ProductsCrudService: CrudService<Product>
{
    public ProductsCrudService(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(ProductType category)
    {
        return await Context.Products.Where(p => p.Category == category).ToListAsync();
    }
}