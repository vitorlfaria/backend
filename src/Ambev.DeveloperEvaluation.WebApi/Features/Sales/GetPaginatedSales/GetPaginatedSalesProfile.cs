using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// AutoMapper profile for mapping between Sale entity, DTOs, and API models for paginated sales.
/// </summary>
public class GetPaginatedSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedSales feature.
    /// </summary>
    public GetPaginatedSalesProfile()
    {
        CreateMap<Sale, GetPaginatedSalesResult>();
        CreateMap<Sale, GetPaginatedSalesResponse>();
        CreateMap<GetPaginatedSalesResult, GetPaginatedSalesResponse>();
        CreateMap<PaginatedList<GetPaginatedSalesResult>, PaginatedListResponse<GetPaginatedSalesResponse>>()
            .ForMember(dest => dest.Items, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                {
                    return src.Select(item => context.Mapper.Map<GetPaginatedSalesResponse>(item)).ToList();
                }))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.CurrentPage));
        CreateMap<GetPaginatedSalesRequest, GetPaginatedSalesCommand>();
    }
}