using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.DbHelper;


AppDbContext dbContext = new AppDbContext();
IDbHelper dbHelper = new EfDbHelper(dbContext);
        
List<prebuild_patterns> prebuild_patterns = dbHelper.GetAllPrebuildPatternsAsync().Result; 

foreach (var pp in prebuild_patterns)
{
    Console.WriteLine("---------------------------------------------");
    Console.WriteLine("Serial number: " + pp.serial_number);
    Console.WriteLine("Name: " + pp.prebuild_name);
    Console.WriteLine("Manufacturer: " + pp.manufacturer);
    Console.WriteLine("Description: " + pp.description);
    Console.WriteLine("Price: " + pp.base_price);
    Console.WriteLine("Image: " + pp.img_url);
    
    Console.WriteLine("Pattern components: ");
    foreach (var ppPc in pp.pattern_components)
    {
        Console.WriteLine("   " + ppPc.component.manufacturer);
    }
    
    Console.WriteLine("Product SKU: " + pp.products?.sku);
    Console.WriteLine("---------------------------------------------\n");
}
