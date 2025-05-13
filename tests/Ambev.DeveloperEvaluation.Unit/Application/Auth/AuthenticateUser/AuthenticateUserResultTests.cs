using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

/// <summary>
/// Contains unit tests for the AuthenticateUserResult class.
/// Tests cover result creation and property assignment.
/// </summary>
public class AuthenticateUserResultTests
{
    /// <summary>
    /// Tests that the AuthenticateUserResult is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "AuthenticateUserResult should be created with the correct properties")]
    public void Given_Properties_When_ResultCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var token = "testToken";
        var id = Guid.NewGuid();
        var name = "TestUser";
        var email = "test@example.com";
        var phone = "+1234567890";
        var role = "Admin";

        // Act
        var result = new AuthenticateUserResult
        {
            Token = token,
            Id = id,
            Name = name,
            Email = email,
            Phone = phone,
            Role = role
        };

        // Assert
        Assert.Equal(token, result.Token);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);
        Assert.Equal(phone, result.Phone);
        Assert.Equal(role, result.Role);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
    // For example, testing default values when properties are not explicitly set.
}