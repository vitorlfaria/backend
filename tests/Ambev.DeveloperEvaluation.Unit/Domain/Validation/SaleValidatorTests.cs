using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of sale properties, including customer ID, branch ID, sale date, total amount, and sale items.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the customer ID is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty customer ID")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveValidationErrorForCustomerId()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithEmptyCustomerId();

        // Act & Assert
        _validator.TestValidate(sale).ShouldHaveValidationErrorFor(s => s.CustomerId);
    }

    /// <summary>
    /// Tests that validation fails when the branch ID is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty branch ID")]
    public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveValidationErrorForBranchId()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithEmptyBranchId();

        // Act & Assert
        _validator.TestValidate(sale).ShouldHaveValidationErrorFor(s => s.BranchId);
    }

    /// <summary>
    /// Tests that validation fails when the sale date is in the future.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for future sale date")]
    public void Given_FutureSaleDate_When_Validated_Then_ShouldHaveValidationErrorForSaleDate()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithFutureDate();

        // Act & Assert
        _validator.TestValidate(sale).ShouldHaveValidationErrorFor(s => s.SaleDate);
    }

    /// <summary>
    /// Tests that validation fails when the total amount is zero or negative.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for zero or negative total amount")]
    public void Given_ZeroOrNegativeTotalAmount_When_Validated_Then_ShouldHaveValidationErrorForTotalAmount()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithInvalidTotalAmount();

        // Act & Assert
        _validator.TestValidate(sale).ShouldHaveValidationErrorFor(s => s.TotalAmount);
    }

    /// <summary>
    /// Tests that validation fails when there are no sale items.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty sale items")]
    public void Given_EmptySaleItems_When_Validated_Then_ShouldHaveValidationErrorForSaleItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateSaleWithEmptyItems();

        // Act & Assert
        _validator.TestValidate(sale).ShouldHaveValidationErrorFor(s => s.SaleItems);
    }
}