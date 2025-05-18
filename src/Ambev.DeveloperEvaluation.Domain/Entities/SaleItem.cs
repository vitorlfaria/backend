using System;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale.
/// </summary>
public class SaleItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This property is used to link the sale item to a specific product in the system.
    /// Must not be null or empty.
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the denormilized name of the product.
    /// This property is used to display the product name in the system.
    /// Must not be null or empty.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

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
        CalculateDiscount();
        TotalPrice = UnitPrice * Quantity * (1 - (Discount / 100m));
    }

    /// <summary>
    /// Calculates the discount percentage based on the quantity of items purchased.
    /// </summary>
    /// <remarks>
    /// The discount is calculated as follows:
    /// - Purchases above 4 identical items have a 10% discount
    /// - Purchases between 10 and 20 identical items have a 20% discount
    /// - It's not possible to sell above 20 identical items
    /// - Purchases below 4 items cannot have a discount
    /// </remarks>
    private void CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            Discount = 20;
        else if (Quantity > 4)
            Discount = 10;
        else
            Discount = 0;
    }
}
