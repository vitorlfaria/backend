using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleItemRequest, SaleItemDto>()
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Discount, opt => opt.Ignore());
        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}