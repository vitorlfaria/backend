namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation.
/// </summary>
public class GetSaleResponse
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
    /// The name of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The name of the branch where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    /// <summary>
    /// The list of items associated with the sale.
    /// </summary>
    public List<GetSaleItemResponse> SaleItems { get; set; } = new();
}

/// <summary>
/// Represents an item in a sale.
/// </summary>
public class GetSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the Sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique identifier of the product associated with the item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The name of the product associated with the item.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product in the item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product in the item.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the item.
    /// </summary>
    public decimal Discount { get; set; }
}