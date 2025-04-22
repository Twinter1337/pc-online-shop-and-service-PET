namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;

public class PrebuildPatternDto
{
    public int SerialNumber { get; set; }
    public string PrebuildName { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal BasePrice { get; set; }
}