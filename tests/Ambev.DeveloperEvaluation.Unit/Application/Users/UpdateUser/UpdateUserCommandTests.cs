using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.UpdateUser;

/// <summary>
/// Contains unit tests for the UpdateUserCommand class.
/// Tests cover initialization and validation scenarios.
/// </summary>
public class UpdateUserCommandTests
{
    /// <summary>
    /// Tests that validation passes when all UpdateUserCommand properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid UpdateUserCommand data")]
    public void Given_ValidUpdateUserCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            Username = "UpdatedUser",
            Phone = "+19876543210",
            Email = "updated@email.com",
            Status = UserStatus.Inactive,
            Role = UserRole.Admin
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when UpdateUserCommand properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid UpdateUserCommand data")]
    public void Given_InvalidUpdateUserCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.Empty, // Invalid: empty Guid
            Username = "", // Invalid: empty
            Phone = "123", // Invalid: doesn't match pattern
            Email = "invalid-email", // Invalid: not a valid email
            Status = UserStatus.Unknown, // Invalid: cannot be Unknown
            Role = UserRole.None // Invalid: cannot be None
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}