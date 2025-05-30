using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// AutoMapper profile for mapping between UpdateSaleCommand, Sale, and UpdateSaleResult.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleProfile class.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalItems, opt => opt.Ignore());

        CreateMap<Sale, UpdateSaleResult>();

        CreateMap<UpdateSaleItemCommand, SaleItem>()
            .ForMember(dest => dest.SaleId, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

        CreateMap<SaleItem, UpdateSaleItemResult>();
    }
}