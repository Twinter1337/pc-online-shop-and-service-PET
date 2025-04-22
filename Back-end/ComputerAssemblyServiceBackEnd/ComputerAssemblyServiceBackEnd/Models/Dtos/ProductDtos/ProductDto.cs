using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;

public class ProductDto
{
    public int Sku { get; set; }
    public int? ComputerId { get; set; }
    public int? ComponentId { get; set; }
    public ProductType Category { get; set; }
    public string ImgUrl { get; set; } = null!;
}