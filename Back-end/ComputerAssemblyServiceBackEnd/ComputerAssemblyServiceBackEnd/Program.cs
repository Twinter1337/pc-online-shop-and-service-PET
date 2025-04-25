using AutoMapper;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Mappings;
using ComputerAssemblyServiceBackEnd.Models.Dtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;

public class Program
{
    public static void Main()
    {
        // ComponentsCrudService ccs = new ComponentsCrudService(new AppDbContext());
        //
        // var comp = new ComponentUpdateDto()
        // {
        //     Category = ComponentType.Case.ToString(),
        //     Manufacturer = "Computer",
        //     Model = "Computer",
        //     Price = 10000,
        //     QuantityOnStock = 100,
        //     Characteristics = new Dictionary<string, object>() { { "spec1", 100 } }
        // };
        //
        // await ccs.UpdateEntityAsync(47, comp);
    }
}