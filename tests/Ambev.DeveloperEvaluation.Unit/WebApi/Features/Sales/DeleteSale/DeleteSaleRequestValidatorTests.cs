using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleRequestValidator class.
/// Tests cover validation rules for the sale ID in a delete request.
/// </summary>
public class DeleteSaleRequestValidatorTests
{
    private readonly DeleteSaleRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public DeleteSaleRequestValidatorTests()
    {
        _validator = new DeleteSaleRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid sale ID is provided in the delete request.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale ID")]
    public void Given_ValidSaleId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new DeleteSaleRequest { Id = Guid.NewGuid() };

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