using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class ProductsService: IProductsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public ProductsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetProductsByCategoryAsync(ProductType category)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }
}