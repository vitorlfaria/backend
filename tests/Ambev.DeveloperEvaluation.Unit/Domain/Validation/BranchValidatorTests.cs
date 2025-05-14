using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the BranchValidator class.
/// Tests cover validation of branch properties, specifically the name.
/// </summary>
public class BranchValidatorTests
{
    private readonly BranchValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public BranchValidatorTests()
    {
        _validator = new BranchValidator();
    }

    /// <summary>
    /// Tests that validation passes when the branch name is valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid branch name")]
    public void Given_ValidBranchName_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the branch name is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty or invalid branch name")]
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveValidationErrorForName()
    {
        // Arrange
        var branch = BranchTestData.GenerateBranchWithEmptyName();

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Name);
    }
}