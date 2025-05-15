using System;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This property is used to link the sale item to a specific product in the system.
    /// Must not be null or empty.
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// This property is used to link the sale item to a specific sale in the system.
    /// Must not be null or empty.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// Must be a positive integer.
    /// </summary>
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// Must be a positive decimal value.
    /// </summary>
    public decimal UnitPrice { get; set; } = 0;

    /// <summary>
    /// Gets the total price of the sale item.
    /// This property is calculated based on the unit price, quantity, and discount.
    /// </summary>
    public decimal TotalPrice { get; private set; } = 0;

    /// <summary>
    /// Gets or sets the discount percentage applied to the sale item.
    /// Must be a positive integer between 0 and 100.
    /// </summary>
    public int Discount { get; set; }

    /// <summary>
    /// Calculates the total price of the sale item.
    /// </summary>
    /// <remarks>
    /// The total price is calculated as UnitPrice * Quantity * (1 - (Discount/ 100m)).
    /// </remarks>
    public void CalculateTotalPrice()
    {
        TotalPrice = UnitPrice * Quantity * (1 - (Discount / 100m));
    }

    // Navigation properties

    /// <summary>
    /// Gets or sets the product associated with the sale item.
    /// This property is used to link the sale item to a specific product in the system.
    /// </summary>
    public virtual Product Product { get; set; } = new();

    /// <summary>
    /// Gets or sets the sale associated with the sale item.
    /// This property is used to link the sale item to a specific sale in the system.
    /// </summary>
    public virtual Sale Sale { get; set; } = new();
}
