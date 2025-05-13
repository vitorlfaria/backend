using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

/// <summary>
/// Contains unit tests for the AuthenticateUserProfile class.
/// Tests cover mapping configuration and property mapping.
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
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticateUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that the User entity is correctly mapped to the AuthenticateUserResult.
    /// </summary>
    [Fact(DisplayName = "User should be mapped to AuthenticateUserResult correctly")]
    public void Given_UserEntity_When_MappedToResult_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticateUserProfile>());
        var mapper = config.CreateMapper();
        var user = new User { Username = "TestUser", Email = "test@example.com", Role = UserRole.Admin };

        // Act
        var result = mapper.Map<AuthenticateUserResult>(user);

        // Assert
        Assert.Equal(user.Username, result.Name);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Role.ToString(), result.Role);
        Assert.Equal(Guid.Empty, result.Id); // Assuming Id is not mapped
        Assert.Equal(string.Empty, result.Token); // Assuming Token is ignored
    }
}