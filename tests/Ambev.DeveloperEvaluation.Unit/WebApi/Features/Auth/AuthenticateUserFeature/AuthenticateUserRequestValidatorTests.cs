using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// Contains unit tests for the AuthenticateUserRequestValidator class.
/// Tests cover validation rules for email and password.
/// </summary>
public class AuthenticateUserRequestValidatorTests
{
    private readonly AuthenticateUserRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public AuthenticateUserRequestValidatorTests()
    {
        _validator = new AuthenticateUserRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when the email and password are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid email and password")]
    public void Given_ValidEmailAndPassword_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new AuthenticateUserRequest { Email = "test@example.com", Password = "password123" };

        // Act
        var result = _validator.TestValidate(request);

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
        var request = new AuthenticateUserRequest { Email = "", Password = "password123" };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Email);
    }

    /// <summary>
    /// Tests that validation fails when the password is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty password")]
    public void Given_EmptyPassword_When_Validated_Then_ShouldHaveValidationErrorForPassword()
    {
        // Arrange
        var request = new AuthenticateUserRequest { Email = "test@example.com", Password = "" };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Password);
    }
}