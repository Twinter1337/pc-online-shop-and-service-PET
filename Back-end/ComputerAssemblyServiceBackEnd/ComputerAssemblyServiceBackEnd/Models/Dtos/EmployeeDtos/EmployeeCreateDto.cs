using System.ComponentModel.DataAnnotations;

namespace ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeeDtos;

public class EmployeeCreateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string BankAccount { get; set; } = null!;
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public int Position { get; set; }
    [Required]
    public DateOnly HireDate { get; set; }
}