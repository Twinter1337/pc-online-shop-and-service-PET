using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class ProductPatchDto
{
    public int? ComputerId { get; set; }
    public int? ComponentId { get; set; }
    public ProductType? Category { get; set; }
    public string? ImgUrl { get; set; }
}