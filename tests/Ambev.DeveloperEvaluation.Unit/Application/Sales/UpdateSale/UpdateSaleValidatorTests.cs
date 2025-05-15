using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using FluentValidation.TestHelper;
using Xunit;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.UpdateSale;

/// <summary>
/// Contains unit tests for the UpdateSaleCommandValidator class.
/// Tests cover validation rules for the UpdateSaleCommand.
/// </summary>
public class UpdateSaleValidatorTests
{
    private readonly UpdateSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public UpdateSaleValidatorTests()
    {
        _validator = new UpdateSaleCommandValidator();
    }

    /// <summary>
    /// Tests that validation passes when the UpdateSaleCommand has valid data.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid UpdateSaleCommand data")]
    public void Given_ValidUpdateSaleCommandData_When_Validated_Then_ShouldNotHaveValidationErrors()
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
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the UpdateSaleCommand has an empty ID.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty sale ID")]
    public void Given_EmptySaleId_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var command = new UpdateSaleCommand { Id = Guid.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required.");
    }

    // You can add more tests here to cover other validation rules,
    // such as testing the validation of SaleItems, or specific rules
    // for Number, SaleDate, CustomerId, TotalAmount, BranchId, and Canceled.
    // For example:
    // [Fact(DisplayName = "Validation should fail for invalid sale number")]
    // public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveValidationErrorForNumber()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for future sale date")]
    // public void Given_FutureSaleDate_When_Validated_Then_ShouldHaveValidationErrorForSaleDate()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for empty customer ID")]
    // public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveValidationErrorForCustomerId()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for zero total amount")]
    // public void Given_ZeroTotalAmount_When_Validated_Then_ShouldHaveValidationErrorForTotalAmount()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for empty branch ID")]
    // public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveValidationErrorForBranchId()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for empty sale items")]
    // public void Given_EmptySaleItems_When_Validated_Then_ShouldHaveValidationErrorForSaleItems()
    // {
    //     // Arrange & Act & Assert
    // }
    // [Fact(DisplayName = "Validation should fail for invalid sale item quantity")]
    // public void Given_InvalidSaleItemQuantity_When_Validated_Then_ShouldHaveValidationErrorForSaleItemsQuantity()
    // {
    //     // Arrange & Act & Assert
    // }
}