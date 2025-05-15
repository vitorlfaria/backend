using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.CreateSale;

/// <summary>
/// Contains unit tests for the CreateSaleResult class.
/// Tests cover property initialization and default values.
/// </summary>
public class CreateSaleResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_CreateSaleResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();
        var result = new CreateSaleResult { Id = id };

        // Act & Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Number property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Number property should be initialized correctly")]
    public void Given_CreateSaleResult_When_NumberIsSet_Then_NumberShouldBeCorrect()
    {
        // Arrange
        var number = 12345;
        var result = new CreateSaleResult { Number = number };

        // Act & Assert
        Assert.Equal(number, result.Number);
    }

    /// <summary>
    /// Tests that the SaleDate property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "SaleDate property should be initialized correctly")]
    public void Given_CreateSaleResult_When_SaleDateIsSet_Then_SaleDateShouldBeCorrect()
    {
        // Arrange
        var saleDate = DateTime.UtcNow.AddDays(-1);
        var result = new CreateSaleResult { SaleDate = saleDate };

        // Act & Assert
        Assert.Equal(saleDate, result.SaleDate);
    }
}