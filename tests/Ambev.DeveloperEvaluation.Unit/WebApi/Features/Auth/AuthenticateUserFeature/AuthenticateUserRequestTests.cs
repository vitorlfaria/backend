using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// Contains unit tests for the AuthenticateUserRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class AuthenticateUserRequestTests
{
    /// <summary>
    /// Tests that the AuthenticateUserRequest is created with the correct Email and Password.
    /// </summary>
    [Fact(DisplayName = "AuthenticateUserRequest should be created with the correct Email and Password")]
    public void Given_EmailAndPassword_When_RequestCreated_Then_ShouldHaveCorrectEmailAndPassword()
    {
        // Arrange
        var email = "test@example.com";
        var password = "password123";

        // Act & Assert
        var request = new AuthenticateUserRequest { Email = email, Password = password };
        Assert.Equal(email, request.Email);
        Assert.Equal(password, request.Password);
    }
}