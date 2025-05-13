using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.CreateUser;

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
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that CreateUserCommand is correctly mapped to User.
    /// </summary>
    [Fact(DisplayName = "CreateUserCommand should be mapped to User correctly")]
    public void Given_CreateUserCommand_When_MappedToUser_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserProfile>());
        var mapper = config.CreateMapper();
        var command = new CreateUserCommand { Username = "TestUser", Password = "password", Email = "test@example.com", Phone = "+1234567890" };

        // Act
        var user = mapper.Map<User>(command);

        // Assert
        Assert.Equal(command.Username, user.Username);
        Assert.Equal(command.Password, user.Password);
        Assert.Equal(command.Email, user.Email);
        Assert.Equal(command.Phone, user.Phone);
    }
}