using ComputerAssemblyServiceBackEnd;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;

Console.WriteLine("Hello World!");
//
AppDbContext appDbContext = new AppDbContext();
//
// PrebuildPatternsCrudService ppcs = new PrebuildPatternsCrudService(appDbContext);
// ComponentsCrudService ccs = new ComponentsCrudService(appDbContext);
// ServicesCrudService scs = new ServicesCrudService(appDbContext);
// BogusDataGenerator bds = new BogusDataGenerator();
//
// List<Component> components = bds.GenerateComponentsData(10);
//
// foreach (Component component in components)
// {
//     await ccs.CreateEntityAsync(component);
//     // Console.WriteLine(component.Category + " " + component.Manufacturer + " " + component.Model + " " + component.Price + " " + component.QuantityOnStock);
// }

var xmlExporter = new XmlTableExporter(appDbContext);

await xmlExporter.ExportTableToXmlFileAsync<Component>();

var csvExporter = new CsvTableExporter(appDbContext);

await csvExporter.ExportTableToCsvFileAsync<Component>();