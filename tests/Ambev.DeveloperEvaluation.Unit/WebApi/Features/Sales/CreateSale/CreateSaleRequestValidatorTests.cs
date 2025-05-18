using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Contains unit tests for the CreateSaleRequestValidator class.
/// Tests cover validation rules for sale properties during creation.
/// </summary>
public class CreateSaleRequestValidatorTests
{
    private readonly CreateSaleRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public CreateSaleRequestValidatorTests()
    {
        _validator = new CreateSaleRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Number = 123,
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Test Customer",
            BranchId = Guid.NewGuid(),
            BranchName = "Test Branch",
            SaleItems = [new SaleItemRequest { ProductId = Guid.NewGuid(), ProductName = "Test Product", Quantity = 2, UnitPrice = 25.00m }]
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the sale number is invalid.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for invalid sale number")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveValidationErrorForNumber(int number)
    {
        // Arrange
        var request = new CreateSaleRequest { Number = number };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Number);
    }

    /// <summary>
    /// Tests that validation fails when the sale date is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid sale date")]
    public void Given_InvalidSaleDate_When_Validated_Then_ShouldHaveValidationErrorForSaleDate()
    {
        // Arrange
        var request = new CreateSaleRequest { SaleDate = DateTime.UtcNow.AddDays(1) };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.SaleDate);
    }

    /// <summary>
    /// Tests that validation fails when the customer ID is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty customer ID")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveValidationErrorForCustomerId()
    {
        // Arrange
        var request = new CreateSaleRequest { CustomerId = Guid.Empty };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.CustomerId);
    }
}