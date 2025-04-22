namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;

public class EmployeePositionDto
{
    public int PositionId { get; set; }
    public string PositionName { get; set; } = null!;
    public int MaximumNumberOfEmpolyees { get; set; }
}