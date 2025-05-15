using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesResult class.
/// Tests cover property initialization.
/// </summary>
public class GetPaginatedSalesResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_GetPaginatedSalesResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = new GetPaginatedSalesResult { Id = id };

        // Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Number property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Number property should be initialized correctly")]
    public void Given_GetPaginatedSalesResult_When_NumberIsSet_Then_NumberShouldBeCorrect()
    {
        // Arrange
        var number = 123;

        // Act
        var result = new GetPaginatedSalesResult { Number = number };

        // Assert
        Assert.Equal(number, result.Number);
    }

    /// <summary>
    /// Tests that the SaleDate property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "SaleDate property should be initialized correctly")]
    public void Given_GetPaginatedSalesResult_When_SaleDateIsSet_Then_SaleDateShouldBeCorrect()
    {
        // Arrange
        var saleDate = DateTime.UtcNow;

        // Act
        var result = new GetPaginatedSalesResult { SaleDate = saleDate };

        // Assert
        Assert.Equal(saleDate, result.SaleDate);
    }

    // You can add more tests here to cover other properties of GetPaginatedSalesResult,
    // such as CustomerId, TotalAmount, BranchId, and Canceled.
    // [Fact(DisplayName = "CustomerId property should be initialized correctly")]
    // public void Given_GetPaginatedSalesResult_When_CustomerIdIsSet_Then_CustomerIdShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "TotalAmount property should be initialized correctly")]
    // public void Given_GetPaginatedSalesResult_When_TotalAmountIsSet_Then_TotalAmountShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "BranchId property should be initialized correctly")]
    // public void Given_GetPaginatedSalesResult_When_BranchIdIsSet_Then_BranchIdShouldBeCorrect() { /* ... */ }
    // [Fact(DisplayName = "Canceled property should be initialized correctly")]
    // public void Given_GetPaginatedSalesResult_When_CanceledIsSet_Then_CanceledShouldBeCorrect() { /* ... */ }
}