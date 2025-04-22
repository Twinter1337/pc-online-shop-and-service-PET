namespace ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

public class EmployeePositionPatchDto
{
    public string? PositionName { get; set; } = null!;
    public int? MaximumNumberOfEmpolyees { get; set; }
}