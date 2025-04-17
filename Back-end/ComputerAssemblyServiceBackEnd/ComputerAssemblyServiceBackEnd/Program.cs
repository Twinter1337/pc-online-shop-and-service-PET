using System.Text.Json.Nodes;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Services.Source;


AppDbContext dbContext = new AppDbContext();
// IDbHelper dbHelper = new EfDbHelper(dbContext);
//         
// List<prebuild_patterns> prebuild_patterns = dbHelper.GetAllPrebuildPatternsAsync().Result; 
//
// foreach (var pp in prebuild_patterns)
// {
//     Console.WriteLine("---------------------------------------------");
//     Console.WriteLine("Serial number: " + pp.serial_number);
//     Console.WriteLine("Name: " + pp.prebuild_name);
//     Console.WriteLine("Manufacturer: " + pp.manufacturer);
//     Console.WriteLine("Description: " + pp.description);
//     Console.WriteLine("Price: " + pp.base_price);
//     Console.WriteLine("Image: " + pp.img_url);
//     
//     Console.WriteLine("Components: ");
//     foreach (var ppPc in pp.pattern_components)
//     {
//         Console.WriteLine("   " + ppPc.component.category + ": " + ppPc.component.manufacturer + " " + ppPc.component.model);
//         Console.WriteLine("      Characteristics: ");
//         for (int i = 0; i < ppPc.component.characteristics.Count; i++)
//         {
//             Console.WriteLine("         " + ppPc.component.characteristics[i]?.GetPropertyName() + ": " + ppPc.component.characteristics[i]);
//         }
//     }
//     
//     Console.WriteLine("Product SKU: " + pp.products?.sku);
//     Console.WriteLine("---------------------------------------------\n");
// }

// ComponentsService componentsService = new ComponentsService(dbContext);
//
// var cpnt = new Component()
// {
//     ComponentId = 8,
//     Manufacturer = "ASUS",
//     Model = "ROG",
//     Category = ComponentType.GPU,
//     QuantityOnStock = 11,
//     Price = 10000,
//     Characteristics = new JsonObject()
// };
//
// bool res = await componentsService.UpdateComponentAsync(8, cpnt);
//
// Console.WriteLine(res);
//
// List<Component> components = await componentsService.GetAllComponentsAsync();
//
// foreach (var c in components)
// {
//     Console.WriteLine(c.Model);
// }
