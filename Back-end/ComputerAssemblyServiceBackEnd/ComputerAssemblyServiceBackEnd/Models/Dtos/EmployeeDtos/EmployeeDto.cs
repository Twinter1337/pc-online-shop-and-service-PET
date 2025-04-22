namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeeDtos;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public int UserId { get; set; }
    public string BankAccount { get; set; } = null!;
    public decimal Salary { get; set; }
    public int Position { get; set; }
    public DateOnly HireDate { get; set; }
}