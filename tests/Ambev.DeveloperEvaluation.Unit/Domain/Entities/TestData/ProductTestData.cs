using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Product entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Name (using product names)
    /// - Description (using product descriptions)
    /// - Price (using random decimals greater than 0)
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Finance.Amount(min: 0.01m));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a Product entity with an empty name for testing invalid scenarios.
    /// </summary>
    /// <returns>A Product entity with an empty name.</returns>
    public static Product GenerateProductWithEmptyName()
    {
        var product = GenerateValidProduct();
        product.Name = string.Empty;
        return product;
    }

    /// <summary>
    /// Generates a Product entity with a zero or negative price for testing invalid scenarios.
    /// </summary>
    /// <returns>A Product entity with a zero or negative price.</returns>
    public static Product GenerateProductWithInvalidPrice()
    {
        var product = GenerateValidProduct();
        product.Price = 0;
        return product;
    }

    public static Product GenerateProductWithEmptyDescription()
    {
        var product = GenerateValidProduct();
        product.Description = string.Empty;
        return product;
    }
}
