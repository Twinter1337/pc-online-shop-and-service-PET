namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class EmployeeFilter
{
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public int? Position { get; set; }
    public DateOnly? MinHireDate { get; set; }
    public DateOnly? MaxHireDate { get; set; }
}