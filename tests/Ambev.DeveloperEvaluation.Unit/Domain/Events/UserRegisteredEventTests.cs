using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Events;

/// <summary>
/// Contains unit tests for the UserRegisteredEvent class.
/// Tests cover event creation and property assignment.
/// </summary>
public class UserRegisteredEventTests
{
    /// <summary>
    /// Tests that the UserRegisteredEvent is created with the correct User.
    /// </summary>
    [Fact(DisplayName = "UserRegisteredEvent should be created with the correct User")]
    public void Given_User_When_EventCreated_Then_ShouldHaveCorrectUser()
    {
        // Arrange
        var user = new User { Username = "TestUser" };

        // Act
        var userRegisteredEvent = new UserRegisteredEvent(user);

        // Assert
        Assert.Equal(user, userRegisteredEvent.User);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.

}
