using AutoMapper;
using ComputerAssemblyServiceBackEnd.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComponentDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ComputerOnServiceDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeeDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.EmployeePositionDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderItemDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderServiceDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatternComponentDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PaymentDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ServiceDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.UserDtos;

namespace ComputerAssemblyServiceBackEnd.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Component

        CreateMap<Component, ComponentDto>()
            .AfterMap((src, dest) =>
            {
                dest.Specs = src.Characteristics != null
                    ? ConvertJsonObjectToDictionary(src.Characteristics)
                    : new Dictionary<string, object>();
            });

        CreateMap<ComponentDto, Component>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = (JsonObject?)JsonNode.Parse(JsonSerializer.Serialize(src.Specs));
            });

        CreateMap<Component, ComponentCreateDto>()
            .AfterMap((src, dest) =>
            {
                dest.Specs = src.Characteristics != null
                    ? ConvertJsonObjectToDictionary(src.Characteristics)
                    : new Dictionary<string, object>();
            });

        CreateMap<ComponentCreateDto, Component>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = (JsonObject?)JsonNode.Parse(JsonSerializer.Serialize(src.Specs));
            });

        CreateMap<Component, ComponentUpdateDto>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = src.Characteristics != null
                    ? ConvertJsonObjectToDictionary(src.Characteristics)
                    : new Dictionary<string, object>();
            });

        CreateMap<ComponentUpdateDto, Component>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = (JsonObject?)JsonNode.Parse(JsonSerializer.Serialize(src.Characteristics));
            });

        CreateMap<Component, ComponentPatchDto>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = src.Characteristics != null
                    ? ConvertJsonObjectToDictionary(src.Characteristics)
                    : new Dictionary<string, object>();
            });

        CreateMap<ComponentPatchDto, Component>()
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = (JsonObject?)JsonNode.Parse(JsonSerializer.Serialize(src.Characteristics));
            });

        #endregion

        #region ComputerOnService

        CreateMap<ComputerOnService, ComputerOnServiceDto>().ReverseMap();
        CreateMap<ComputerOnService, ComputerOnServiceCreateDto>().ReverseMap();
        CreateMap<ComputerOnService, ComputerOnServiceUpdateDto>().ReverseMap();
        CreateMap<ComputerOnService, ComputerOnServicePatchDto>().ReverseMap();

        #endregion

        #region Employee

        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
        CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
        CreateMap<Employee, EmployeePatchDto>().ReverseMap();

        #endregion

        #region EmployeePosition

        CreateMap<EmployeePosition, EmployeePositionDto>().ReverseMap();
        CreateMap<EmployeePosition, EmployeePositionCreateDto>().ReverseMap();
        CreateMap<EmployeePosition, EmployeePositionUpdateDto>().ReverseMap();
        CreateMap<EmployeePosition, EmployeePositionPatchDto>().ReverseMap();

        #endregion

        #region Order

        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();
        CreateMap<Order, OrderPatchDto>().ReverseMap();

        #endregion

        #region OrderItem

        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemUpdateDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemPatchDto>().ReverseMap();

        #endregion

        #region OrderService

        CreateMap<OrderService, OrderServiceDto>().ReverseMap();
        CreateMap<OrderService, OrderServiceCreateDto>().ReverseMap();
        CreateMap<OrderService, OrderServiceUpdateDto>().ReverseMap();
        CreateMap<OrderService, OrderServicePatchDto>().ReverseMap();

        #endregion

        #region PatternComponent

        CreateMap<PatternComponent, PatternComponentDto>().ReverseMap();
        CreateMap<PatternComponent, PatternComponentCreateDto>().ReverseMap();
        CreateMap<PatternComponent, PatternComponentUpdateDto>().ReverseMap();
        CreateMap<PatternComponent, PatternComponentPatchDto>().ReverseMap();

        #endregion

        #region Payment

        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Payment, PaymentCreateDto>().ReverseMap();
        CreateMap<Payment, PaymentUpdateDto>().ReverseMap();
        CreateMap<Payment, PaymentPatchDto>().ReverseMap();

        #endregion

        #region PrebuildPattern

        CreateMap<PrebuildPattern, PrebuildPatternDto>().ReverseMap();
        CreateMap<PrebuildPattern, PrebuildPatternCreateDto>().ReverseMap();
        CreateMap<PrebuildPattern, PrebuildPatternUpdateDto>().ReverseMap();
        CreateMap<PrebuildPattern, PrebuildPatternPatchDto>().ReverseMap();

        #endregion

        #region Product

        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
        CreateMap<Product, ProductPatchDto>().ReverseMap();

        #endregion

        #region Service

        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<Service, ServiceCreateDto>().ReverseMap();
        CreateMap<Service, ServiceUpdateDto>().ReverseMap();
        CreateMap<Service, ServicePatchDto>().ReverseMap();

        #endregion

        #region User

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserPatchDto>().ReverseMap();

        #endregion
    }

    private static Dictionary<string, object> ConvertJsonObjectToDictionary(JsonObject jsonObject)
    {
        var dictionary = new Dictionary<string, object>();
        foreach (var property in jsonObject)
        {
            dictionary[property.Key] = property.Value?.ToString() ?? string.Empty;
        }

        return dictionary;
    }
}