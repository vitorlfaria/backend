using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetSale;

/// <summary>
/// Contains unit tests for the GetSaleRequestValidator class.
/// Tests cover validation rules for the sale ID in a get sale request.
/// </summary>
public class GetSaleRequestValidatorTests
{
    private readonly GetSaleRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public GetSaleRequestValidatorTests()
    {
        _validator = new GetSaleRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid sale ID is provided in the get sale request.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale ID")]
    public void Given_ValidSaleId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new GetSaleRequest { Id = Guid.NewGuid() };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Id);
    }

    // Additional tests for invalid scenarios can be added here
    // For example, testing with an empty Guid:
    // [Fact(DisplayName = "Validation should fail for empty sale ID")]
    // public void Given_EmptySaleId_When_Validated_Then_ShouldHaveValidationErrorForId() { ... }
}