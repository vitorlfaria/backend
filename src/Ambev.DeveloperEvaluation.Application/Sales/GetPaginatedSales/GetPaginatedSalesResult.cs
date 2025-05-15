namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

/// <summary>
/// Represents a sale in a paginated list.
/// </summary>
public class GetPaginatedSalesResult
{
    /// <summary>
    /// The unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The number of the sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }
}