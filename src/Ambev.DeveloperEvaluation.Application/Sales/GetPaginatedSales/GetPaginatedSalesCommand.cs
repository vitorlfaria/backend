using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

/// <summary>
/// Command to retrieve a paginated list of sales.
/// </summary>
public record GetPaginatedSalesCommand : IRequest<PaginatedList<GetPaginatedSalesResult>>
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