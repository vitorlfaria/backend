using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserProfile class.
/// Tests cover mapping configurations between API requests and application commands.
/// </summary>
public class DeleteUserProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the DeleteUserProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeleteUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that a Guid (representing the user ID) is correctly mapped to a DeleteUserCommand.
    /// </summary>
    [Fact(DisplayName = "Guid should be mapped to DeleteUserCommand correctly")]
    public void Given_UserIdGuid_When_MappedToCommand_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeleteUserProfile>());
        var mapper = config.CreateMapper();
        var userId = Guid.NewGuid();

        // Act
        var command = mapper.Map<DeleteUserCommand>(userId);

        // Assert
        Assert.Equal(userId, command.Id);
    }
}