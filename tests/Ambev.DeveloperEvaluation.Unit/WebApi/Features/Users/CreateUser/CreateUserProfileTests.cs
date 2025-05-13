using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users.CreateUser;

/// <summary>
/// Contains unit tests for the CreateUserProfile class.
/// Tests cover mapping configurations between API requests/responses and application commands/results.
/// </summary>
public class CreateUserProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the CreateUserProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Users.CreateUser.CreateUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that CreateUserRequest is correctly mapped to CreateUserCommand.
    /// </summary>
    [Fact(DisplayName = "CreateUserRequest should be mapped to CreateUserCommand correctly")]
    public void Given_CreateUserRequest_When_MappedToCommand_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Users.CreateUser.CreateUserProfile>());
        var mapper = config.CreateMapper();
        var request = new CreateUserRequest { Username = "TestUser", Password = "password", Email = "test@example.com", Phone = "+1234567890" };

        // Act
        var command = mapper.Map<CreateUserCommand>(request);

        // Assert
        Assert.Equal(request.Username, command.Username);
        Assert.Equal(request.Password, command.Password);
        Assert.Equal(request.Email, command.Email);
        Assert.Equal(request.Phone, command.Phone);
    }
}