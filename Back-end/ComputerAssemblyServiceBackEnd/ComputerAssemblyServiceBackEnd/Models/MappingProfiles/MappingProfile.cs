using AutoMapper;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;
using System.Text.Json;
using System.Text.Json.Nodes;
using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Component
        CreateMap<Component, ComponentDto>()
            .ForMember(dest => dest.ComponentId, opt => opt.MapFrom(src => src.ComponentId))
            .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.QuantityOnStock, opt => opt.MapFrom(src => src.QuantityOnStock))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .AfterMap((src, dest) =>
            {
                dest.Specs = src.Characteristics != null
                    ? ConvertJsonObjectToDictionary(src.Characteristics)
                    : new Dictionary<string, object>();
            });

        CreateMap<ComponentDto, Component>()
            .ForMember(dest => dest.ComponentId, opt => opt.MapFrom(src => src.ComponentId))
            .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.QuantityOnStock, opt => opt.MapFrom(src => src.QuantityOnStock))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src =>
                Enum.Parse<ComponentType>(src.Category)))
            .AfterMap((src, dest) =>
            {
                dest.Characteristics = (JsonObject?)JsonNode.Parse(JsonSerializer.Serialize(src.Specs));
            });
        
        //ComputerOnService
        CreateMap<ComputerOnService, ComputerOnServiceDto>();
        CreateMap<ComputerOnServiceDto, ComputerOnService>();
        
        //Employee
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
        
        //EmployeePosition
        CreateMap<EmployeePosition, EmployeePositionDto>();
        CreateMap<EmployeePositionDto, EmployeePosition>();
        
        //Order
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
        
        //OrderItem
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItem>();
        
        //OrderService
        CreateMap<OrderService, OrderServiceDto>();
        CreateMap<OrderServiceDto, OrderService>();
        
        //PatternComponent
        CreateMap<PatternComponent, PatternComponentDto>();
        CreateMap<PatternComponentDto, PatternComponent>();
        
        //Payment
        CreateMap<Payment, PaymentDto>();
        CreateMap<PaymentDto, Payment>();
        
        //PrebuildPattern
        CreateMap<PrebuildPattern, PrebuildPatternDto>();
        CreateMap<PrebuildPatternDto, PrebuildPattern>();
        
        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        
        //Service
        CreateMap<Service, ServiceDto>();
        CreateMap<ServiceDto, Service>();
        
        //User
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
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