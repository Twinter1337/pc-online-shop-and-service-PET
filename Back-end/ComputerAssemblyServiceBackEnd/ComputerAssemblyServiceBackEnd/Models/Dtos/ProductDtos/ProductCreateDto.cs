using System.ComponentModel.DataAnnotations;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;

public class ProductCreateDto
{
    public int ComputerId { get; set; }
    [Required]
    public ProductType Category { get; set; }
    public string ImgUrl { get; set; } = null!;
}