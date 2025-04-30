using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Npgsql.Internal;

Console.WriteLine("Hello World!");

// AppDbContext context = new AppDbContext();
// PrebuildPatternsCrudService ppcs = new PrebuildPatternsCrudService(context);
// ComponentsCrudService ccs = new ComponentsCrudService(context);
// PatternComponentsCrudService pccs = new PatternComponentsCrudService(context);
//
// List<PrebuildPattern> prebuildPatterns = await ppcs.GetAllEntitiesAsync();
//
// List<Component> components = await ccs.GetAllEntitiesAsync();
//
// List<Component> cpus = components.Where(c => c.Category == ComponentType.CPU).ToList();
// List<Component> gpus = components.Where(c => c.Category == ComponentType.GPU).ToList();
// List<Component> psus = components.Where(c => c.Category == ComponentType.PSU).ToList();
// List<Component> motherboards = components.Where(c => c.Category == ComponentType.Motherboard).ToList();
// List<Component> rams = components.Where(c => c.Category == ComponentType.RAM).ToList();
// List<Component> cases = components.Where(c => c.Category == ComponentType.Case).ToList();
// List<Component> coolingSystems = components.Where(c => c.Category == ComponentType.CoolingSystem).ToList();
// List<Component> hdds = components.Where(c => c.Category == ComponentType.HDD).ToList();
// List<Component> ssds = components.Where(c => c.Category == ComponentType.SSD).ToList();
//
// Random random = new Random();
//
// foreach (var pattern in prebuildPatterns)
// {
//     List<Component> selectedComponents = new List<Component>
//     {
//         cpus[random.Next(cpus.Count)],
//         gpus[random.Next(gpus.Count)],
//         psus[random.Next(psus.Count)],
//         motherboards[random.Next(motherboards.Count)],
//         rams[random.Next(rams.Count)],
//         cases[random.Next(cases.Count)],
//         coolingSystems[random.Next(coolingSystems.Count)],
//         hdds[random.Next(hdds.Count)],
//         ssds[random.Next(ssds.Count)]
//     };
//     
//     foreach (var component in selectedComponents)
//     {
//         PatternComponent patternComponent = new PatternComponent
//         {
//             PatternId = pattern.SerialNumber,
//             ComponentId = component.ComponentId,
//             Quantity = 1
//         };
//         
//         await pccs.CreateEntityAsync(patternComponent);
//     }
// }