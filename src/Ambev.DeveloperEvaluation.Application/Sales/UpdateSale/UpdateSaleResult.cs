namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the result of an update sale operation.
/// </summary>
public class UpdateSaleResult
{
    /// <summary>
    /// Gets or sets the ID of the updated sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the number of the updated sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the updated sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the updated sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the updated sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the updated sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the updated sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }
}