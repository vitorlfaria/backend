using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.CreateUser;

/// <summary>
/// Contains unit tests for the CreateUserCommand class.
/// Tests cover validation scenarios.
/// </summary>
public class CreateUserCommandTests
{
    /// <summary>
    /// Tests that validation passes when all CreateUserCommand properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid CreateUserCommand data")]
    public void Given_ValidCreateUserCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "ValidUser",
            Password = "ValidPassword123!",
            Email = "valid@email.com",
            Phone = "+1234567890",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when CreateUserCommand properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid CreateUserCommand data")]
    public void Given_InvalidCreateUserCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "", // Invalid: empty
            Password = "short", // Invalid: too short
            Email = "invalid-email", // Invalid: not a valid email
            Phone = "123", // Invalid: doesn't match pattern
            Status = UserStatus.Unknown, // Invalid: cannot be Unknown
            Role = UserRole.None // Invalid: cannot be None
        };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    // Additional tests can be added to cover specific validation rules in more detail.
    // For example, testing the password complexity rules individually.
    // Or testing the exact error messages returned for each invalid property.
}