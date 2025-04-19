using AutoMapper;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Mappings;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

public class Program
{
    public static void Main()
    {
// AppDbContext dbContext = new AppDbContext();
//
// var config = new MapperConfiguration(cfg =>
//     cfg.AddProfile<MappingProfile>());
//
// IMapper mapper = config.CreateMapper();
//
// ComponentsCrudService crudService = new ComponentsCrudService(dbContext);
//
// List<Component> c = await crudService.GetAllEntitiesAsync();
//
// ComponentDto cDto = mapper.Map<ComponentDto>(c[0]);
//
// Console.WriteLine("Component to DTO:");
//
// Console.WriteLine(cDto.Category + " " + cDto.Manufacturer + " " + cDto.Model + " " + cDto.Price + " " +
//                   cDto.ComponentId + " " + cDto.QuantityOnStock);
//
// foreach (var s in cDto.Specs)
// {
//     Console.WriteLine(s.Key + " " + s.Value);
// }
//
// Console.WriteLine("DTO to Component:");
//
// Component comp = mapper.Map<Component>(cDto);
//
// Console.WriteLine(comp.Category + " " + comp.Manufacturer + " " + comp.Model + " " + comp.Price + " " +
//                   comp.ComponentId + " " + comp.QuantityOnStock + " " + comp.Characteristics);
    }
}