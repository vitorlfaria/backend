using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.UpdateUser;

/// <summary>
/// Contains unit tests for the UpdateUserResult class.
/// Tests cover property initialization.
/// </summary>
public class UpdateUserResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_UpdateUserResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = new UpdateUserResult { Id = id };

        // Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Username property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Username property should be initialized correctly")]
    public void Given_UpdateUserResult_When_UsernameIsSet_Then_UsernameShouldBeCorrect()
    {
        // Arrange
        var username = "UpdatedUser";

        // Act
        var result = new UpdateUserResult { Username = username };

        // Assert
        Assert.Equal(username, result.Username);
    }

    /// <summary>
    /// Tests that the Phone property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Phone property should be initialized correctly")]
    public void Given_UpdateUserResult_When_PhoneIsSet_Then_PhoneShouldBeCorrect()
    {
        // Arrange
        var phone = "+1122334455";

        // Act
        var result = new UpdateUserResult { Phone = phone };

        // Assert
        Assert.Equal(phone, result.Phone);
    }
}