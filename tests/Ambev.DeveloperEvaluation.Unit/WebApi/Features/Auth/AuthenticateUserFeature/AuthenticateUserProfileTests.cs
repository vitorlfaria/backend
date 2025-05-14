using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// Contains unit tests for the AuthenticateUserProfile class in the WebApi layer.
/// Tests cover mapping configurations between API requests/responses and application commands/results.
/// </summary>
public class AuthenticateUserProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the AuthenticateUserProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature.AuthenticateUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that AuthenticateUserRequest is correctly mapped to AuthenticateUserCommand.
    /// </summary>
    [Fact(DisplayName = "AuthenticateUserRequest should be mapped to AuthenticateUserCommand correctly")]
    public void Given_AuthenticateUserRequest_When_MappedToCommand_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature.AuthenticateUserProfile>());
        var mapper = config.CreateMapper();
        var request = new AuthenticateUserRequest { Email = "test@example.com", Password = "password" };

        // Act
        var command = mapper.Map<AuthenticateUserCommand>(request);

        // Assert
        Assert.Equal(request.Email, command.Email);
        Assert.Equal(request.Password, command.Password);
    }
}