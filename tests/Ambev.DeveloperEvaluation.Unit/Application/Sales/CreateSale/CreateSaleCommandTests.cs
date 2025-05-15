using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Xunit;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.CreateSale;

/// <summary>
/// Contains unit tests for the CreateSaleCommand class.
/// Tests cover validation scenarios.
/// </summary>
public class CreateSaleCommandTests
{
    /// <summary>
    /// Tests that validation passes when all CreateSaleCommand properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid CreateSaleCommand data")]
    public void Given_ValidCreateSaleCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Number = 12345,
            SaleDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.25m, Discount = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.00m, Discount = 5 }
            },
            Canceled = false
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when CreateSaleCommand properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid CreateSaleCommand data")]
    public void Given_InvalidCreateSaleCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Number = 0, // Invalid: zero
            SaleDate = DateTime.UtcNow.AddDays(1), // Invalid: future date
            CustomerId = Guid.Empty, // Invalid: empty Guid
            TotalAmount = 0, // Invalid: zero
            BranchId = Guid.Empty, // Invalid: empty Guid
            SaleItems = new List<SaleItemDto>(), // Invalid: empty list
            Canceled = false
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when CreateSaleCommand has invalid SaleItems.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid SaleItems in CreateSaleCommand")]
    public void Given_InvalidSaleItemsInCreateSaleCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Number = 12345,
            SaleDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.Empty, Quantity = 0, UnitPrice = 0, Discount = 110 }, // Invalid items
            },
            Canceled = false
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}