namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update an existing sale.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the ID of the sale to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the new number for the sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the new date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the new unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the denormalized customer name associated with the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the new unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the denormalized branch name where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new list of product identifiers included in the sale.
    /// </summary>
    public List<UpdateSaleItemRequest> SaleItems { get; set; } = [];

    /// <summary>
    /// Gets or sets a flag indicating whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }
}

public class UpdateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Discount { get; set; }
}