using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.CreateUser;

/// <summary>
/// Contains unit tests for the CreateUserRequestValidator class.
/// Tests cover validation rules for user properties during creation.
/// </summary>
public class CreateUserRequestValidatorTests
{
    private readonly CreateUserRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public CreateUserRequestValidatorTests()
    {
        _validator = new CreateUserRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when all user properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid user data")]
    public void Given_ValidUserData_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Username = "TestUser",
            Password = "Password123!",
            Phone = "+551234567890",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the email is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid email")]
    public void Given_InvalidEmail_When_Validated_Then_ShouldHaveValidationErrorForEmail()
    {
        // Arrange
        var request = new CreateUserRequest { Email = "invalid-email" };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Email);
    }

    /// <summary>
    /// Tests that validation fails when the username is empty or has invalid length.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for empty or invalid username")]
    [InlineData("")]
    [InlineData("ab")]
    [InlineData("ThisUsernameIsWayTooLongAndExceedsTheMaximumAllowedLength")]
    public void Given_InvalidUsername_When_Validated_Then_ShouldHaveValidationErrorForUsername(string username)
    {
        // Arrange
        var request = new CreateUserRequest { Username = username };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Username);
    }

    /// <summary>
    /// Tests that validation fails when the password does not meet security requirements.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for weak password")]
    [InlineData("weak")]
    [InlineData("no-uppercase")]
    [InlineData("NoSpecialChar1")]
    [InlineData("NoNumbers!")]
    public void Given_WeakPassword_When_Validated_Then_ShouldHaveValidationErrorForPassword(string password)
    {
        // Arrange
        var request = new CreateUserRequest { Password = password };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Password);
    }

    /// <summary>
    /// Tests that validation fails when the phone number has an invalid format.
    /// </summary>
    [Theory(DisplayName = "Validation should fail for invalid phone number format")]
    [InlineData("+0123456789")]
    [InlineData("1")]
    [InlineData("+1 (415) 555-2671")]
    [InlineData("+1-415-555-2671")]
    public void Given_InvalidPhoneNumber_When_Validated_Then_ShouldHaveValidationErrorForPhone(string phone)
    {
        // Arrange
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Username = "TestUser",
            Password = "Password123!",
            Phone = phone,
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Phone);
    }
}