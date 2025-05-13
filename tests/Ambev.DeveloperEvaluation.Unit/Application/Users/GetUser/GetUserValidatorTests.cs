using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserValidator class.
/// Tests cover validation rules for the user ID.
/// </summary>
public class GetUserValidatorTests
{
    private readonly GetUserValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public GetUserValidatorTests()
    {
        _validator = new GetUserValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid user ID is provided.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid user ID")]
    public void Given_ValidUserId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new GetUserCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    /// <summary>
    /// Tests that validation fails when an empty user ID is provided.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty user ID")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var command = new GetUserCommand(Guid.Empty);

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Id);
    }
}