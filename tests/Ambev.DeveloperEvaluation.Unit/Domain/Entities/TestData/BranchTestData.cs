using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Branch entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class BranchTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated branches will have valid:
    /// - Name (using company names)
    /// </summary>
    private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName());

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static Branch GenerateValidBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Generates a Branch entity with an empty name for testing invalid scenarios.
    /// </summary>
    /// <returns>A Branch entity with an empty name.</returns>
    public static Branch GenerateBranchWithEmptyName()
    {
        return new Branch { Name = string.Empty };
    }
}