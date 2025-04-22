using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;

public class PrebuildPatternCreateDto
{
    [Required]
    public string PrebuildName { get; set; } = null!;
    [Required]
    public string Manufacturer { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public decimal BasePrice { get; set; }
}