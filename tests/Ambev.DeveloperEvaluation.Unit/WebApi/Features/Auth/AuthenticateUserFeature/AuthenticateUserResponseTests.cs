using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// Contains unit tests for the AuthenticateUserResponse class.
/// Tests cover response creation and property assignment.
/// </summary>
public class AuthenticateUserResponseTests
{
    /// <summary>
    /// Tests that the AuthenticateUserResponse is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "AuthenticateUserResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var token = "testToken";
        var email = "test@example.com";
        var name = "TestUser";
        var role = "Admin";

        // Act
        var response = new AuthenticateUserResponse
        {
            Token = token,
            Email = email,
            Name = name,
            Role = role
        };

        // Assert
        Assert.Equal(token, response.Token);
        Assert.Equal(email, response.Email);
        Assert.Equal(name, response.Name);
        Assert.Equal(role, response.Role);
    }
}