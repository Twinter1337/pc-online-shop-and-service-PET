namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class PrebuildPatternFilter
{
    public string? Manufacturer { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}