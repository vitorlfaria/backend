using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.UpdateSale;

/// <summary>
/// Contains unit tests for the UpdateSaleResult class.
/// Tests cover property initialization.
/// </summary>
public class UpdateSaleResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_UpdateSaleResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();
        var result = new UpdateSaleResult { Id = id };

        // Act & Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Number property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Number property should be initialized correctly")]
    public void Given_UpdateSaleResult_When_NumberIsSet_Then_NumberShouldBeCorrect()
    {
        // Arrange
        var number = 54321;
        var result = new UpdateSaleResult { Number = number };

        // Act & Assert
        Assert.Equal(number, result.Number);
    }

    /// <summary>
    /// Tests that the SaleDate property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "SaleDate property should be initialized correctly")]
    public void Given_UpdateSaleResult_When_SaleDateIsSet_Then_SaleDateShouldBeCorrect()
    {
        // Arrange
        var saleDate = DateTime.UtcNow.AddDays(-2);
        var result = new UpdateSaleResult { SaleDate = saleDate };

        // Act & Assert
        Assert.Equal(saleDate, result.SaleDate);
    }

    /// <summary>
    /// Tests that UpdateDaleItemResult is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "UpdateSaleItemResult should be initialized correctly")]
    public void Given_UpdateSaleItemResult_When_Initialized_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 10;
        var unitPrice = 19.99m;
        var discount = 10;
        var itemResult = new UpdateSaleItemResult
        {
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Discount = discount
        };

        // Act & Assert
        Assert.Multiple(
            () => Assert.Equal(productId, itemResult.ProductId),
            () => Assert.Equal(quantity, itemResult.Quantity),
            () => Assert.Equal(unitPrice, itemResult.UnitPrice),
            () => Assert.Equal(discount, itemResult.Discount)
        );
    }
}