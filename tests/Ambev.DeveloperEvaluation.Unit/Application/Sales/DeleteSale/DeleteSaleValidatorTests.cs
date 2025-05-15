using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentValidation.TestHelper;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleValidator class.
/// Tests cover validation rules for the DeleteSaleCommand.
/// </summary>
public class DeleteSaleValidatorTests
{
    private readonly DeleteSaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public DeleteSaleValidatorTests()
    {
        _validator = new DeleteSaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when the DeleteSaleCommand has a valid ID.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid DeleteSaleCommand data")]
    public void Given_ValidDeleteSaleCommandData_When_Validated_Then_ShouldNotHaveValidationErrorForId()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    /// <summary>
    /// Tests that validation fails when the DeleteSaleCommand has an empty ID.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid DeleteSaleCommand data")]
    public void Given_InvalidDeleteSaleCommandData_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.Empty); // Invalid: empty ID

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required");
    }
}