using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetPaginatedProducts;

/// <summary>
/// Command to retrieve a paginated list of products.
/// </summary>
public record GetPaginatedProductsCommand : IRequest<PaginatedList<GetPaginatedProductsResult>>
{
    /// <summary>
    /// The page number to retrieve.
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; init; } = 10;
}