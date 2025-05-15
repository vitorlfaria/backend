using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

/// <summary>
/// AutoMapper profile for mapping between Sale entity and GetPaginatedSalesResult.
/// </summary>
public class GetPaginatedSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedSales operation.
    /// </summary>
    public GetPaginatedSalesProfile()
    {
        CreateMap<Sale, GetPaginatedSalesResult>();
    }
}