using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserRequestValidator class.
/// Tests cover validation rules for the user ID in a get user request.
/// </summary>
public class GetUserRequestValidatorTests
{
    private readonly GetUserRequestValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserRequestValidatorTests"/> class.
    /// Sets up the validator for testing.
    /// </summary>
    public GetUserRequestValidatorTests()
    {
        _validator = new GetUserRequestValidator();
    }

    /// <summary>
    /// Tests that validation passes when a valid user ID is provided in the get user request.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid user ID")]
    public void Given_ValidUserId_When_Validated_Then_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new GetUserRequest { Id = Guid.NewGuid() };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Id);
    }

    /// <summary>
    /// Tests that validation fails when an empty user ID is provided in the get user request.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty user ID")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveValidationErrorForId()
    {
        // Arrange
        var request = new GetUserRequest { Id = Guid.Empty };

        // Act & Assert
        _validator.TestValidate(request).ShouldHaveValidationErrorFor(r => r.Id);
    }
}