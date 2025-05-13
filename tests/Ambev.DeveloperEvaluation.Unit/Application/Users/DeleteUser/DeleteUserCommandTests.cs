using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserCommand class.
/// Tests cover command creation and property assignment.
/// </summary>
public class DeleteUserCommandTests
{
    /// <summary>
    /// Tests that the DeleteUserCommand is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "DeleteUserCommand should be created with the correct Id")]
    public void Given_UserId_When_CommandCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var command = new DeleteUserCommand(userId);

        // Assert
        Assert.Equal(userId, command.Id);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
    // For example, if the command had other properties besides Id.
}