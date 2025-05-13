using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserValidator class.
/// Tests cover validation rules for the user ID.
/// </summary>
public class DeleteUserValidatorTests
{
    private readonly DeleteUserValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public DeleteUserValidatorTests()
    {
        _validator = new DeleteUserValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid user ID is provided.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid user ID")]
    public void Given_ValidUserId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.NewGuid());

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
        var command = new DeleteUserCommand(Guid.Empty);

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Id);
    }
}