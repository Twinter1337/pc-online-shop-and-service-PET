using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum ProductType
{
    [PgName("Computer")]
    Computer,
    [PgName("Gaming PC")]
    GamingPC,
    [PgName("Work station")]
    WorkStation,
}