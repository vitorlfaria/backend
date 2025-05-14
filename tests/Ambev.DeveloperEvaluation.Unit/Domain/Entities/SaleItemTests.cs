using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the SaleItem entity class.
    /// Tests cover properties, initialization, and total price calculation.
    /// </summary>
    public class SaleItemTests
    {
        /// <summary>
        /// Tests that a SaleItem object can be created with the correct properties.
        /// </summary>
        [Fact(DisplayName = "Should create a sale item with the correct properties")]
        public void CreateSaleItem_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var quantity = 3;
            var unitPrice = 25.50m;
            var discount = 10;

            // Act
            var saleItem = new SaleItem
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Discount = discount
            };

            // Assert
            saleItem.Should().NotBeNull();
            saleItem.ProductId.Should().Be(productId);
            saleItem.Quantity.Should().Be(quantity);
            saleItem.UnitPrice.Should().Be(unitPrice);
            saleItem.Discount.Should().Be(discount);
        }

        /// <summary>
        /// Tests that the total price of a sale item is calculated correctly.
        /// </summary>
        [Fact(DisplayName = "Should calculate the total price correctly")]
        public void CalculateTotalPrice_ShouldReturnCorrectValue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var quantity = 2;
            var unitPrice = 50.00m;
            var discount = 5;

            var saleItem = new SaleItem
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Discount = discount
            };

            // Act & Assert
            saleItem.CalculateTotalPrice();
            saleItem.TotalPrice.Should().Be(95.00m); // (50.00 * 2) * (1 - 0.05) = 95.00
        }
    }
}