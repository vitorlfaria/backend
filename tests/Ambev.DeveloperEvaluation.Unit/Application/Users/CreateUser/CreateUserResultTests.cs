using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.CreateUser;

/// <summary>
/// Contains unit tests for the CreateUserResult class.
/// Tests cover property initialization and default values.
/// </summary>
public class CreateUserResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_CreateUserResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();
        var result = new CreateUserResult { Id = id };

        // Act & Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Name property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Name property should be initialized correctly")]
    public void Given_CreateUserResult_When_NameIsSet_Then_NameShouldBeCorrect()
    {
        // Arrange
        var name = "Test User";
        var result = new CreateUserResult { Name = name };

        // Act & Assert
        Assert.Equal(name, result.Name);
    }

    /// <summary>
    /// Tests that the Email property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Email property should be initialized correctly")]
    public void Given_CreateUserResult_When_EmailIsSet_Then_EmailShouldBeCorrect()
    {
        // Arrange
        var email = "test@example.com";
        var result = new CreateUserResult { Email = email };

        // Act & Assert
        Assert.Equal(email, result.Email);
    }

    /// <summary>
    /// Tests that the Phone property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Phone property should be initialized correctly")]
    public void Given_CreateUserResult_When_PhoneIsSet_Then_PhoneShouldBeCorrect()
    {
        // Arrange
        var phone = "+1234567890";
        var result = new CreateUserResult { Phone = phone };

        // Act & Assert
        Assert.Equal(phone, result.Phone);
    }


    /// <summary>
    /// Tests that the Role property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Role property should be initialized correctly")]
    public void Given_CreateUserResult_When_RoleIsSet_Then_RoleShouldBeCorrect()
    {
        // Arrange
        var role = UserRole.Customer;
        var result = new CreateUserResult { Role = role };
        // Act & Assert
        Assert.Equal(role, result.Role);
    }

    /// <summary>
    /// Tests that the Status property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Status property should be initialized correctly")]
    public void Given_CreateUserResult_When_StatusIsSet_Then_StatusShouldBeCorrect()
    {
        // Arrange
        var status = UserStatus.Active;
        var result = new CreateUserResult { Status = status };

        // Act & Assert
        Assert.Equal(status, result.Status);
    }
}