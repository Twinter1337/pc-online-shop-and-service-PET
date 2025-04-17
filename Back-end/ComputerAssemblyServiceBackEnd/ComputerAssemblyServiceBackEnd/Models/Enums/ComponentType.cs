using System.Runtime.Serialization;
using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum ComponentType
{
    [PgName("GPU")]
    GPU,
    [PgName("CPU")]
    CPU,
    [PgName("RAM")]
    RAM,
    [PgName("Motherboard")]
    Motherboard,
    [PgName("HDD")]
    HDD,
    [PgName("SSD")]
    SSD,
    [PgName("PSU")]
    PSU,
    [PgName("Case")]
    Case,
    [PgName("CoolingSystem")]
    CoolingSystem
}