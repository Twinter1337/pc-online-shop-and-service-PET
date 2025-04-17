using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum PaymentMethod
{
    [PgName("Card")]
    Card, 
    [PgName("Cash")]
    Cash, 
    [PgName("Crypto")]
    Crypto 
}