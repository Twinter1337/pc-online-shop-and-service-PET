namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;

public class EmployeePositionCreateDto
{
    public string PositionName { get; set; } = null!;
    public int MaximumNumberOfEmpolyees { get; set; }
}