using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// AutoMapper profile for mapping between Sale entity and GetPaginatedSalesResult.
/// AutoMapper profile for mapping between Sale entity, DTOs, and API models for paginated sales.
/// </summary>
public class GetPaginatedSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedSales operation.
    /// Initializes the mappings for GetPaginatedSales feature.
    /// </summary>
    public GetPaginatedSalesProfile()
    {
        CreateMap<Sale, GetPaginatedSalesResult>();
        CreateMap<Sale, GetPaginatedSalesResponse>();
        CreateMap<PaginatedList<GetPaginatedSalesResult>, PaginatedListResponse<GetPaginatedSalesResponse>>()
            .ForMember(dest => dest.Items, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                {
                    var items = context.Items["Items"] as List<GetPaginatedSalesResponse>;
                    if (items == null)
                    {
                        items = new List<GetPaginatedSalesResponse>();
                        context.Items["Items"] = items;
                    }
                    foreach (var item in src)
                    {
                        var mappedItem = context.Mapper.Map<GetPaginatedSalesResponse>(item);
                        items.Add(mappedItem);
                    }

                    return items;
                }))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.CurrentPage));
        CreateMap<GetPaginatedSalesRequest, GetPaginatedSalesCommand>();
    }
}