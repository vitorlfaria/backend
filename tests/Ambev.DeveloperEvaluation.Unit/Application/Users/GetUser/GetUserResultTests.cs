using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserResult class.
/// Tests cover result creation and property assignment.
/// </summary>
public class GetUserResultTests
{
    /// <summary>
    /// Tests that the GetUserResult is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "GetUserResult should be created with the correct properties")]
    public void Given_Properties_When_ResultCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "TestUser";
        var email = "test@example.com";
        var phone = "+1234567890";
        var role = UserRole.Customer;
        var status = UserStatus.Active;

        // Act
        var result = new GetUserResult
        {
            Id = id,
            Name = name,
            Email = email,
            Phone = phone,
            Role = role,
            Status = status
        };

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);
        Assert.Equal(phone, result.Phone);
        Assert.Equal(role, result.Role);
        Assert.Equal(status, result.Status);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
    // For example, testing default values when properties are not explicitly set.
    // Or testing the behavior of the class if invalid values are assigned (if applicable).
}