using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// Gets or sets the ID of the created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the number of the created sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the created sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the created sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer associated with the created sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the created sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the total number of items sold in the created sale.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the created sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch where the created sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a flag indicating whether the created sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    /// <summary>
    /// Gets or sets the list of items associated with the created sale.
    /// </summary>
    public List<SaleItemDto> SaleItems { get; set; } = [];
}