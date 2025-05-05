using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd;

public class PatternComponentsDataGenerator
{
    private Random _random = new();
    
    List<PatternComponent> GenerateData(List<PrebuildPattern> prebuildPatterns, List<Component> components)
    {
        List<PatternComponent> patternComponents = new List<PatternComponent>();
        
        List<Component> cpus = components.Where(c => c.Category == ComponentType.CPU).ToList();
        List<Component> gpus = components.Where(c => c.Category == ComponentType.GPU).ToList();
        List<Component> psus = components.Where(c => c.Category == ComponentType.PSU).ToList();
        List<Component> motherboards = components.Where(c => c.Category == ComponentType.Motherboard).ToList();
        List<Component> rams = components.Where(c => c.Category == ComponentType.RAM).ToList();
        List<Component> cases = components.Where(c => c.Category == ComponentType.Case).ToList();
        List<Component> coolingSystems = components.Where(c => c.Category == ComponentType.CoolingSystem).ToList();
        List<Component> hdds = components.Where(c => c.Category == ComponentType.HDD).ToList();
        List<Component> ssds = components.Where(c => c.Category == ComponentType.SSD).ToList();

        foreach (var pattern in prebuildPatterns)
        {
            List<Component> selectedComponents = new List<Component>
            {
                cpus[_random.Next(cpus.Count)],
                gpus[_random.Next(gpus.Count)],
                psus[_random.Next(psus.Count)],
                motherboards[_random.Next(motherboards.Count)],
                rams[_random.Next(rams.Count)],
                cases[_random.Next(cases.Count)],
                coolingSystems[_random.Next(coolingSystems.Count)],
                hdds[_random.Next(hdds.Count)],
                ssds[_random.Next(ssds.Count)]
            };
    
            foreach (var component in selectedComponents)
            {
                PatternComponent patternComponent = new PatternComponent
                {
                    PatternId = pattern.SerialNumber,
                    ComponentId = component.ComponentId,
                    Quantity = _random.Next(1, 3),
                };
                
                patternComponents.Add(patternComponent);
            }
        }

        return patternComponents;
    }
}