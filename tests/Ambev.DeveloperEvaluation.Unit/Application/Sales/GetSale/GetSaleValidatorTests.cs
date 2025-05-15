using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation.TestHelper;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetSale;

/// <summary>
/// Contains unit tests for the GetSaleValidator class.
/// Tests cover validation rules for the GetSaleCommand.
/// </summary>
public class GetSaleValidatorTests
{
    private readonly GetSaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public GetSaleValidatorTests()
    {
        _validator = new GetSaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when the GetSaleCommand has a valid ID.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid GetSaleCommand data")]
    public void Given_ValidGetSaleCommandData_When_Validated_Then_ShouldNotHaveValidationErrorForId()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    /// <summary>
    /// Tests that validation fails when the GetSaleCommand has an empty ID.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid GetSaleCommand data")]
    public void Given_InvalidGetSaleCommandData_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.Empty); // Invalid: empty ID

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required");
    }
}