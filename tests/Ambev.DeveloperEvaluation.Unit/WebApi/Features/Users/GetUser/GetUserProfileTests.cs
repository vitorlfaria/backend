using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserProfile class.
/// Tests cover mapping configurations between API requests and application commands.
/// </summary>
public class GetUserProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the GetUserProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Users.GetUser.GetUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that a Guid (representing the user ID) is correctly mapped to a GetUserCommand.
    /// </summary>
    [Fact(DisplayName = "Guid should be mapped to GetUserCommand correctly")]
    public void Given_UserIdGuid_When_MappedToCommand_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Users.GetUser.GetUserProfile>());
        var mapper = config.CreateMapper();
        var userId = Guid.NewGuid();

        // Act
        var command = mapper.Map<GetUserCommand>(userId);

        // Assert
        Assert.Equal(userId, command.Id);
    }
}