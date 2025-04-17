using System.Collections;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.DbHelper;

public interface IDbHelper
{
    public AppDbContext DbContext { get; }

    #region CREATE 
    #endregion

    #region READ

    Task<List<components>> GetAllComponentsAsync();
    Task<List<prebuild_patterns>> GetAllPrebuildPatternsAsync();
    Task<List<computers_on_service>> GetAllComputersOnServiceAsync();
    Task<List<employees>> GetAllEmployeesAsync();
    Task<List<order_items>> GetAllOrderItemsAsync();
    Task<List<orders>> GetAllOrdersAsync();
    Task<List<payments>> GetAllPaymentsAsync();
    Task<List<order_services>> GetAllOrderServicesAsync();
    Task<List<pattern_components>> GetAllPatternComponentsAsync();
    Task<List<products>> GetAllProductsAsync();
    Task<List<services>> GetAllServicesAsync();
    Task<List<users>> GetAllUsersAsync();
    Task<List<employee_positions>> GetAllEmployeePositionsWithEmployeesAsync();

    #endregion

    #region UPDATE

    #endregion

    #region DELETE

    #endregion
}