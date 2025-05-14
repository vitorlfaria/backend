using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class GetUserRequestTests
{
    /// <summary>
    /// Tests that the GetUserRequest is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "GetUserRequest should be created with the correct Id")]
    public void Given_UserId_When_RequestCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act & Assert
        var request = new GetUserRequest { Id = userId };
        Assert.Equal(userId, request.Id);
    }
}