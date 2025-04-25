using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;

public class ProductDto
{
    public int Sku { get; set; }
    public int ComputerId { get; set; }
    public ProductType Category { get; set; }
    public PrebuildPatternDto PrebuildPattern { get; set; } = null!;
    public string ImgUrl { get; set; } = null!;
}