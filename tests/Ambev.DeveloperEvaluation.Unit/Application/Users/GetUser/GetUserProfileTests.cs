using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserProfile class.
/// Tests cover mapping configurations between the User entity and the GetUserResult.
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
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that the User entity is correctly mapped to the GetUserResult.
    /// </summary>
    [Fact(DisplayName = "User should be mapped to GetUserResult correctly")]
    public void Given_UserEntity_When_MappedToResult_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>());
        var mapper = config.CreateMapper();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            Email = "test@example.com",
            Phone = "+1234567890",
            Role = UserRole.Admin,
            Status = UserStatus.Active
        };

        // Act
        var result = mapper.Map<GetUserResult>(user);

        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Name);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Phone, result.Phone);
        Assert.Equal(user.Role, result.Role);
        Assert.Equal(user.Status, result.Status);
    }
}