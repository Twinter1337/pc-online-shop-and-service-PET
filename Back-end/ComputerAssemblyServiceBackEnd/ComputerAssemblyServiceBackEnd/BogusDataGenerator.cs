using Bogus;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd;

public class BogusDataGenerator
{
    private static readonly string[] GpuModels = { "RTX 4080", "RX 7900 XT", "GTX 1660 Super", "Arc A770" };
    private static readonly string[] CpuModels = { "Intel i9-13900K", "Ryzen 9 7950X", "Intel i5-12400F", "Ryzen 5 5600X" };
    private static readonly string[] Manufacturers = { "ASUS", "Gigabyte", "MSI", "Intel", "AMD", "Corsair", "Kingston", "Samsung", "Cooler Master" };
    private static readonly string[] PatternNames = { "Gaming Beast", "Office Essential", "Content Creator Pro", "Budget Build", "Silent Workhorse" };
    private static readonly string[] Services = { "Assembly", "Dust Cleaning", "Thermal Paste Replacement", "Full Diagnostics", "Water Cooling Setup" };

    public List<Component> GenerateComponentsData(int numberOfComponents)
    {
        var componentFaker = new Faker<Component>("en")
            .RuleFor(c => c.Manufacturer, f => f.PickRandom(Manufacturers))
            .RuleFor(c => c.Category, f => f.PickRandom<ComponentType>())
            .RuleFor(c => c.Model, (f, c) =>
            {
                return c.Category switch
                {
                    ComponentType.GPU => f.PickRandom(GpuModels),
                    ComponentType.CPU => f.PickRandom(CpuModels),
                    ComponentType.SSD => $"{f.Company.CompanyName()} {f.Random.Int(256, 2048)}GB NVMe",
                    ComponentType.RAM => $"{f.Random.Int(8, 64)}GB DDR4 {f.Random.Int(2400, 3600)}MHz",
                    ComponentType.PSU => $"{f.Random.Int(550, 1000)}W 80+ Gold",
                    ComponentType.Case => $"{f.Commerce.Color()} Mid-Tower",
                    ComponentType.Motherboard => $"Z{f.Random.Int(490, 790)} ATX",
                    ComponentType.CoolingSystem => $"Liquid Cooler {f.Random.Int(120, 360)}mm",
                    ComponentType.HDD => $"{f.Random.Int(1, 4)}TB SATA HDD",
                    _ => f.Commerce.ProductName()
                };
            })
            .RuleFor(c => c.QuantityOnStock, f => f.Random.Int(5, 50))
            .RuleFor(c => c.Price, f => Math.Round(f.Random.Decimal(500, 25000), 2));

        return componentFaker.Generate(numberOfComponents);
    }

    public List<PrebuildPattern> GeneratePrebuildPatternsData(int numberOfPatterns)
    {
        var patternFaker = new Faker<PrebuildPattern>("en")
            .RuleFor(p => p.PrebuildName, f => f.PickRandom(PatternNames))
            .RuleFor(p => p.Manufacturer, f => f.PickRandom(Manufacturers))
            .RuleFor(p => p.Description, f => $"A prebuilt configuration for {f.PickRandom(new[] { "gaming", "office work", "video editing", "general use", "high performance tasks" })}. Includes hand-picked compatible components.")
            .RuleFor(p => p.BasePrice, f => Math.Round(f.Random.Decimal(5000, 40000), 2));

        return patternFaker.Generate(numberOfPatterns);
    }

    public List<Service> GenerateServicesData(int numberOfServices)
    {
        var serviceFaker = new Faker<Service>("en")
            .RuleFor(s => s.Name, f => f.PickRandom(Services))
            .RuleFor(s => s.Description, f => f.Lorem.Sentence(6) + " Ideal for regular maintenance or upgrades.")
            .RuleFor(s => s.Price, f => Math.Round(f.Random.Decimal(100, 1500), 2));

        return serviceFaker.Generate(numberOfServices);
    }
}