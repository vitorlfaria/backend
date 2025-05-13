using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserCommand class.
/// Tests cover command creation and property assignment.
/// </summary>
public class GetUserCommandTests
{
    /// <summary>
    /// Tests that the GetUserCommand is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "GetUserCommand should be created with the correct Id")]
    public void Given_UserId_When_CommandCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var command = new GetUserCommand(userId);

        // Assert
        Assert.Equal(userId, command.Id);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
    // For example, if the command had other properties besides Id.
}