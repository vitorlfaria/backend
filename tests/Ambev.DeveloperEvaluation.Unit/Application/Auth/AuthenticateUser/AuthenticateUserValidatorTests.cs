using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

/// <summary>
/// Contains unit tests for the AuthenticateUserValidator class.
/// Tests cover validation rules for email and password.
/// </summary>
public class AuthenticateUserValidatorTests
{
    private readonly AuthenticateUserValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public AuthenticateUserValidatorTests()
    {
        _validator = new AuthenticateUserValidator();
    }

    /// <summary>
    /// Tests that validation passes when the email and password are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid email and password")]
    public void Given_ValidEmailAndPassword_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "test@example.com", Password = "password123" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the email is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty email")]
    public void Given_EmptyEmail_When_Validated_Then_ShouldHaveValidationErrorForEmail()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "", Password = "password123" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    /// <summary>
    /// Tests that validation fails when the password is empty or too short.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for empty or short password")]
    [InlineData("")]
    [InlineData("123")]
    public void Given_EmptyOrShortPassword_When_Validated_Then_ShouldHaveValidationErrorForPassword(string password)
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "test@example.com", Password = password };

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Password);
    }
}