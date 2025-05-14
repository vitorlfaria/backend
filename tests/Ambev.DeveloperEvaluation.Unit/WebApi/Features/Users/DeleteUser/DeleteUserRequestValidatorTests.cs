using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserRequestValidator class.
/// Tests cover validation rules for the user ID in a delete request.
/// </summary>
public class DeleteUserRequestValidatorTests
{
    private readonly DeleteUserRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public DeleteUserRequestValidatorTests()
    {
        _validator = new DeleteUserRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid user ID is provided in the delete request.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid user ID")]
    public void Given_ValidUserId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new DeleteUserRequest { Id = Guid.NewGuid() };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Id);
    }

    /// <summary>
    /// Tests that validation fails when an empty user ID is provided in the delete request.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty user ID")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var request = new DeleteUserRequest { Id = Guid.Empty };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Id);
    }
}