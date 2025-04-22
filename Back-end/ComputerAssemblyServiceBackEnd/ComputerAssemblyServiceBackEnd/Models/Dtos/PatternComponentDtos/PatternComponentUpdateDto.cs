namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatternComponentDtos;

public class PatternComponentUpdateDto
{
    public int PatternId { get; set; }
    public int ComponentId { get; set; }
    public int? Quantity { get; set; }
}