namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;

public class EmployeePositionUpdateDto
{
    public string PositionName { get; set; } = null!;
    public int MaximumNumberOfEmpolyees { get; set; }
}