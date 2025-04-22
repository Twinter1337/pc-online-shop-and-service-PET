namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatternComponentDtos;

public class PatternComponentDto
{
    public int PatternComponentId { get; set; }
    public int PatternId { get; set; }
    public int ComponentId { get; set; }
    public int? Quantity { get; set; }
}