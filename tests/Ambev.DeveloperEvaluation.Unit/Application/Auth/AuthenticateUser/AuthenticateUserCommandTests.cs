using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

/// <summary>
/// Contains unit tests for the AuthenticateUserCommand class.
/// Tests cover command creation and property assignment.
/// </summary>
public class AuthenticateUserCommandTests
{
    /// <summary>
    /// Tests that the AuthenticateUserCommand is created with the correct Email and Password.
    /// </summary>
    [Fact(DisplayName = "AuthenticateUserCommand should be created with the correct Email and Password")]
    public void Given_EmailAndPassword_When_CommandCreated_Then_ShouldHaveCorrectEmailAndPassword()
    {
        // Arrange
        var email = "test@example.com";
        var password = "password123";

        // Act & Assert
        var command = new AuthenticateUserCommand { Email = email, Password = password };
        Assert.Equal(email, command.Email);
        Assert.Equal(password, command.Password);
    }
}