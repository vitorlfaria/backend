namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation.
/// </summary>
public class GetSaleResult
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
    /// The denormilized name of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// The total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The total number of items sold in the sale.
    /// </summary>
    public int TotalItems { get; set; }

    /// MyProperty { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The denormilized name of the branch where the sale was made.
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    /// <summary>
    /// The list of items associated with the sale.
    /// </summary>
    public List<GetSaleItemResult> SaleItems { get; set; } = new();
}

/// <summary>
/// Represents an item in a sale.
/// </summary>
public class GetSaleItemResult
{
    /// <summary>
    /// The sale identifier.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique identifier of the product associated with the sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The denormilized name of the product associated with the sale item.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// The quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the product.
    /// </summary>
    public decimal Discount { get; set; }
}