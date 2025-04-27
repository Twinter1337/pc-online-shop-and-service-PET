namespace ComputerAssemblyServiceBackEnd.Filters.Models;

public class OrderItemFilter
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}