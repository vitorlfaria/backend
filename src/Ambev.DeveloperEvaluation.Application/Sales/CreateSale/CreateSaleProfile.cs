using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// AutoMapper profile for mapping between CreateSaleCommand, Sale, and CreateSaleResult.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleProfile class.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalItems, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

        CreateMap<SaleItemDto, SaleItem>()
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Discount, opt => opt.Ignore())
            .ForMember(dest => dest.SaleId, opt => opt.Ignore());

        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<Sale, CreateSaleResult>();
    }
}