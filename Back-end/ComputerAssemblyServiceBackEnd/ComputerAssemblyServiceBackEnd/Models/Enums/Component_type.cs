using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum Component_type
{
    GPU,
    CPU,
    RAM,
    Motherboard,
    HDD,
    SSD,
    PSU,
    Case,
    CoolingSystem
}