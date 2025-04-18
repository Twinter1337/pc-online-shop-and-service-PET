using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ProductsCrudService: CrudService<Product>
{
    public ProductsCrudService(AppDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetProductsByCategoryAsync(ProductType category)
    {
        throw new NotImplementedException();
    }
}