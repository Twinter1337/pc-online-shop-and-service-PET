using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class ProductsCrudService: CrudService<Product>
{
    public ProductsCrudService(AppDbContext context) : base(context)
    {
    }

    public override async Task<bool> UpdateEntityAsync<TDto>(int id, TDto updateDto)
    {
        if (updateDto is not ProductUpdateDto dto)
            return false;

        var dbSet = Context.Set<Product>();

        var existingEntity = await dbSet.FindAsync(id);
        if (existingEntity == null)
            return false;

        existingEntity.Category = dto.Category;
        existingEntity.ImgUrl = dto.ImgUrl;

        if (dto.Category == ProductType.Component)
        {
            if (dto.ComponentId == null || dto.ComputerId != null)
                return false;

            existingEntity.ComponentId = dto.ComponentId;
            existingEntity.ComputerId = null;
        }
        else if (dto.Category == ProductType.Computer)
        {
            if (dto.ComputerId == null || dto.ComponentId != null)
                return false;

            existingEntity.ComputerId = dto.ComputerId;
            existingEntity.ComponentId = null;
        }
        else
        {
            return false;
        }

        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(ProductType category)
    {
        return await Context.Products.Where(p => p.Category == category).ToListAsync();
    }
}