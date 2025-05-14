using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class DeleteUserRequestTests
{
    /// <summary>
    /// Tests that the DeleteUserRequest is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "DeleteUserRequest should be created with the correct Id")]
    public void Given_UserId_When_RequestCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act & Assert
        var request = new DeleteUserRequest { Id = userId };
        Assert.Equal(userId, request.Id);
    }
}