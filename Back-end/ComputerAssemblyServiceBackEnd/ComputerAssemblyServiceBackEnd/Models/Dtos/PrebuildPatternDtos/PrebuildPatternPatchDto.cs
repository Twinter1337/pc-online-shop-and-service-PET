namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class PrebuildPatternPatchDto
{
    public string? PrebuildName { get; set; } = null!;
    public string? Manufacturer { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public decimal? BasePrice { get; set; }
}