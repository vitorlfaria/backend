namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// Represents a request to retrieve a paginated list of sales.
/// </summary>
public class GetPaginatedSalesRequest
{
    /// <summary>
    /// The page number to retrieve. Defaults to 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The number of items per page. Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Initializes a new instance of GetPaginatedSalesRequest.
    /// </summary>
    public GetPaginatedSalesRequest()
    {
    }
}