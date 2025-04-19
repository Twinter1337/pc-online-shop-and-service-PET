namespace ComputerAssemblyServiceBackEnd.Models.Dtos;

public class ServiceDto
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}