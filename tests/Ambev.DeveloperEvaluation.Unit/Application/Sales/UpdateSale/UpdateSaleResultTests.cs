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

    // You can add more tests here to cover other properties of UpdateSaleResult,
    // such as CustomerId, TotalAmount, BranchId, and Canceled.
    // [Fact(DisplayName = "CustomerId property should be initialized correctly")]
    // public void Given_UpdateSaleResult_When_CustomerIdIsSet_Then_CustomerIdShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "TotalAmount property should be initialized correctly")]
    // public void Given_UpdateSaleResult_When_TotalAmountIsSet_Then_TotalAmountShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "BranchId property should be initialized correctly")]
    // public void Given_UpdateSaleResult_When_BranchIdIsSet_Then_BranchIdShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "Canceled property should be initialized correctly")]
    // public void Given_UpdateSaleResult_When_CanceledIsSet_Then_CanceledShouldBeCorrect() { /* ... */ }
}