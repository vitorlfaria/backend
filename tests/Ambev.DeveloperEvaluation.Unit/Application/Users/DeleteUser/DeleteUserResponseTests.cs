using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserResponse class.
/// Tests cover response creation and property assignment.
/// </summary>
public class DeleteUserResponseTests
{
    /// <summary>
    /// Tests that the DeleteUserResponse is created with the correct Success value.
    /// </summary>
    [Theory(DisplayName = "DeleteUserResponse should be created with the correct Success value")]
    [InlineData(true)]
    [InlineData(false)]
    public void Given_SuccessValue_When_ResponseCreated_Then_ShouldHaveCorrectSuccessValue(bool success)
    {
        // Arrange & Act
        var response = new DeleteUserResponse { Success = success };

        // Assert
        Assert.Equal(success, response.Success);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
    // For example, if the response had other properties besides Success.
    // Or testing default values if the Success property is not explicitly set (though it's unlikely in this case).
}