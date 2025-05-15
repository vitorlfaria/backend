using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetPaginatedProducts;

/// <summary>
/// AutoMapper profile for mapping between Product entity and GetPaginatedProductsResult.
/// </summary>
public class GetPaginatedProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedProducts operation.
    /// </summary>
    public GetPaginatedProductsProfile()
    {
        CreateMap<Product, GetPaginatedProductsResult>();
    }
}