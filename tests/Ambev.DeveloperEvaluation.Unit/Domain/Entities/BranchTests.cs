using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Branch entity class.
/// Tests cover basic properties and initialization.
/// </summary>
public class BranchTests
{
    /// <summary>
    /// Tests that a Branch object can be created with the correct name.
    /// </summary>
    [Fact(DisplayName = "Should create a branch with the correct name")]
    public void CreateBranch_ShouldSetNameCorrectly()
    {
        // Arrange
        var branchName = "Main Branch";

        // Act
        var branch = new Branch
        {
            Name = branchName
        };

        // Assert
        branch.Should().NotBeNull();
        branch.Name.Should().Be(branchName);
    }

    /// <summary>
    /// Tests that the branch validation fails when the branch name is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when branch name is empty")]
    public void Validate_EmptyBranchName_ShouldFail()
    {
        // Arrange
        var branch = new Branch { Name = string.Empty };

        // Act & Assert
        branch.Validate().IsValid.Should().BeFalse();
    }
}