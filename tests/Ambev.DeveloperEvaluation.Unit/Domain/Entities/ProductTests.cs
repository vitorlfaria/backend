using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover basic properties, initialization, and validation.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Tests that a Product object can be created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "Should create a product with the correct properties")]
    public void CreateProduct_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var productName = "Test Product";
        var productDescription = "This is a test product.";
        var productPrice = 99.99m;

        // Act
        var product = new Product
        {
            Name = productName,
            Description = productDescription,
            Price = productPrice
        };

        // Assert
        product.Should().NotBeNull();
        product.Name.Should().Be(productName);
        product.Description.Should().Be(productDescription);
        product.Price.Should().Be(productPrice);
    }

    /// <summary>
    /// Tests that the product validation fails when the product name is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when product name is empty")]
    public void Validate_EmptyProductName_ShouldFail()
    {
        // Arrange
        var product = new Product { Name = string.Empty, Description = "Valid Description", Price = 10.0m };

        // Act & Assert
        product.Validate().IsValid.Should().BeFalse();
        product.Validate().Errors.Should().NotBeEmpty();
    }
}