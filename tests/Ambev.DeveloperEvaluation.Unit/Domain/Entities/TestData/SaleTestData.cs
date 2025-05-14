using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Sale entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - Number (using random integers)
    /// - SaleDate (using dates in the past)
    /// - CustomerId (using random GUIDs)
    /// - TotalAmount (using random decimals greater than 0)
    /// - BranchId (using random GUIDs)
    /// - SaleItems (a list of valid SaleItem entities)
    /// - Canceled (randomly true or false)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.Number, f => f.Random.Int(min: 1))
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(min: 0.01m))
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.SaleItems, f => GenerateValidSaleItems(f.Random.Int(min: 1, max: 5)))
        .RuleFor(s => s.Canceled, f => f.Random.Bool());

    /// <summary>
    /// Generates a list of valid SaleItem entities.
    /// </summary>
    /// <param name="count">The number of SaleItem entities to generate.</param>
    /// <returns>A list of valid SaleItem entities.</returns>
    private static List<SaleItem> GenerateValidSaleItems(int count)
    {
        return new Faker<SaleItem>()
            .RuleFor(si => si.ProductId, f => f.Random.Guid())
            .RuleFor(si => si.Quantity, f => f.Random.Int(min: 1))
            .RuleFor(si => si.UnitPrice, f => f.Finance.Amount(min: 0.01m))
            .Generate(count);
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    public static Sale GenerateSaleWithEmptyCustomerId()
    {
        var sale = GenerateValidSale();
        sale.CustomerId = Guid.Empty;
        return sale;
    }

    public static Sale GenerateSaleWithEmptyBranchId()
    {
        var sale = GenerateValidSale();
        sale.BranchId = Guid.Empty;
        return sale;
    }

    public static Sale GenerateSaleWithFutureDate()
    {
        var sale = GenerateValidSale();
        sale.SaleDate = DateTime.UtcNow.AddDays(1);
        return sale;
    }

    public static Sale GenerateSaleWithInvalidTotalAmount()
    {
        var sale = GenerateValidSale();
        sale.TotalAmount = 0;
        return sale;
    }

    public static Sale GenerateSaleWithEmptyItems()
    {
        var sale = GenerateValidSale();
        sale.SaleItems = [];
        return sale;
    }
}