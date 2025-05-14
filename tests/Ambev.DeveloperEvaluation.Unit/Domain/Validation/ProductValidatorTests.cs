using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Tests cover validation of product properties, including name, description, and price.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the product name is empty.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for empty or invalid product name")]
    [InlineData("")]
    public void Given_InvalidProductName_When_Validated_Then_ShouldHaveValidationErrorForName(string name)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Name = name;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    /// <summary>
    /// Tests that validation fails when the product price is zero or negative.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for zero or negative product price")]
    public void Given_ZeroOrNegativeProductPrice_When_Validated_Then_ShouldHaveValidationErrorForPrice()
    {
        // Arrange
        var product = ProductTestData.GenerateProductWithInvalidPrice();

        // Act & Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(p => p.Price);
    }
}