using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.CreateUser;

/// <summary>
/// Contains unit tests for the CreateUserResponse class.
/// Tests cover response creation and property assignment.
/// </summary>
public class CreateUserResponseTests
{
    /// <summary>
    /// Tests that the CreateUserResponse is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "CreateUserResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Test User";
        var email = "test@example.com";
        var phone = "+1 1234567890";
        var role = UserRole.Admin;
        var status = UserStatus.Active;

        // Act
        var response = new CreateUserResponse
        {
            Id = id,
            Name = name,
            Email = email,
            Phone = phone,
            Role = role,
            Status = status
        };

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(name, response.Name);
        Assert.Equal(email, response.Email);
        Assert.Equal(phone, response.Phone);
        Assert.Equal(role, response.Role);
        Assert.Equal(status, response.Status);
    }
}