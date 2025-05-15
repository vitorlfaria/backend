namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This property is used to link the sale item to a specific product in the system.
    /// Must not be null or empty.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// Must be a positive integer.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// Must be a positive decimal value.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to the sale item.
    /// Must be a positive integer between 0 and 100.
    /// </summary>
    public int Discount { get; set; }
}