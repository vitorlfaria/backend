using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.UpdateUser;

/// <summary>
/// Contains unit tests for the UpdateUserProfile class.
/// Tests cover mapping configurations between commands, entities, and results.
/// </summary>
public class UpdateUserProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the UpdateUserProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UpdateUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that UpdateUserCommand is correctly mapped to User.
    /// </summary>
    [Fact(DisplayName = "UpdateUserCommand should be mapped to User correctly")]
    public void Given_UpdateUserCommand_When_MappedToUser_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UpdateUserProfile>());
        var mapper = config.CreateMapper();
        var command = new UpdateUserCommand { Id = Guid.NewGuid(), Username = "UpdatedUser", Phone = "+1122334455", Email = "updated@example.com", Status = UserStatus.Inactive, Role = UserRole.Admin };

        // Act
        var user = mapper.Map<User>(command);

        // Assert
        Assert.Equal(command.Username, user.Username);
        Assert.Equal(command.Phone, user.Phone);
        Assert.Equal(command.Email, user.Email);
        Assert.Equal(command.Status, user.Status);
        Assert.Equal(command.Role, user.Role);
    }
}