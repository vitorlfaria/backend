using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Xunit;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.UpdateSale;

/// <summary>
/// Contains unit tests for the UpdateSaleCommand class.
/// Tests cover initialization and validation scenarios.
/// </summary>
public class UpdateSaleCommandTests
{
    /// <summary>
    /// Tests that validation passes when all UpdateSaleCommand properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid UpdateSaleCommand data")]
    public void Given_ValidUpdateSaleCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            Number = 54321,
            SaleDate = DateTime.UtcNow.AddDays(-2),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 150.75m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.50m, Discount = 5 },
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.12m, Discount = 2 }
            },
            Canceled = true
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when UpdateSaleCommand properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid UpdateSaleCommand data")]
    public void Given_InvalidUpdateSaleCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.Empty, // Invalid: empty Guid
            Number = 0, // Invalid: zero
            SaleDate = DateTime.UtcNow.AddDays(2), // Invalid: future date
            CustomerId = Guid.Empty, // Invalid: empty Guid
            TotalAmount = 0, // Invalid: zero
            BranchId = Guid.Empty, // Invalid: empty Guid
            SaleItems = new List<UpdateSaleItemCommand>(), // Invalid: empty list
            Canceled = false
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when UpdateSaleCommand has invalid SaleItems.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid SaleItems in UpdateSaleCommand")]
    public void Given_InvalidSaleItemsInUpdateSaleCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            Number = 54321,
            SaleDate = DateTime.UtcNow.AddDays(-2),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 150.75m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand { ProductId = Guid.Empty, Quantity = 0, UnitPrice = 0, Discount = 110 }, // Invalid items
            },
            Canceled = true
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    // Additional tests can be added to cover specific validation rules in more detail.
    // For example, testing the exact error messages returned for each invalid property.
    // Or testing boundary conditions for numeric values and date ranges.
}