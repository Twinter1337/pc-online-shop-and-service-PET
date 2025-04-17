using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum ProductType
{
    [PgName("Computer")]
    Computer,
    [PgName("Component")]
    Component
}