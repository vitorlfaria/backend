using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover properties, initialization, and validation.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Tests that a Sale object can be created with the correct properties.
        /// </summary>
        [Fact(DisplayName = "Should create a sale with the correct properties")]
        public void CreateSale_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var saleNumber = 12345;
            var saleDate = DateTime.UtcNow;
            var customerId = Guid.NewGuid();
            var totalAmount = 150.75m;
            var branchId = Guid.NewGuid();
            var saleItems = new List<SaleItem>
            {
                new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 50.00m },
                new SaleItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.75m }
            };
            var canceled = false;

            // Act
            var sale = new Sale
            {
                Number = saleNumber,
                SaleDate = saleDate,
                CustomerId = customerId,
                TotalAmount = totalAmount,
                BranchId = branchId,
                SaleItems = saleItems,
                Canceled = canceled
            };

            // Assert
            sale.Should().NotBeNull();
            sale.Number.Should().Be(saleNumber);
            sale.SaleDate.Should().Be(saleDate);
            sale.CustomerId.Should().Be(customerId);
            sale.TotalAmount.Should().Be(totalAmount);
            sale.BranchId.Should().Be(branchId);
            sale.SaleItems.Should().BeEquivalentTo(saleItems);
            sale.Canceled.Should().Be(canceled);
        }

        /// <summary>
        /// Tests that the sale validation fails when the customer ID is empty.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when customer ID is empty")]
        public void Validate_EmptyCustomerId_ShouldFail()
        {
            // Arrange
            var sale = new Sale
            {
                CustomerId = Guid.Empty,
                BranchId = Guid.NewGuid(),
                TotalAmount = 100.0m,
                SaleItems = [new SaleItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100.0m }]
            };

            // Act & Assert
            sale.Validate().IsValid.Should().BeFalse();
        }
    }
}