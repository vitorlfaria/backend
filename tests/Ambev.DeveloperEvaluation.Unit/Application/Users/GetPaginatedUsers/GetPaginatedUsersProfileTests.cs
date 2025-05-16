using Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetPaginatedUsers;

/// <summary>
/// Contains unit tests for the GetPaginatedUsersProfile class.
/// Tests cover mapping configurations between entities and results.
/// </summary>
public class GetPaginatedUsersProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the GetPaginatedUsersProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GetPaginatedUsersProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that User is correctly mapped to GetPaginatedUsersResult.
    /// </summary>
    [Fact(DisplayName = "User should be mapped to GetPaginatedUsersResult correctly")]
    public void Given_UserEntity_When_MappedToGetPaginatedUsersResult_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetPaginatedUsersProfile>());
        var mapper = config.CreateMapper();
        var user = new User { Id = Guid.NewGuid(), Username = "TestUser", Email = "test@example.com", Phone = "+1234567890", Role = UserRole.Customer, Status = UserStatus.Active };

        // Act
        var result = mapper.Map<GetPaginatedUsersResult>(user);

        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Name); // Note: Mapping Username to Name
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Phone, result.Phone);
        Assert.Equal(user.Role, result.Role);
        Assert.Equal(user.Status, result.Status);
    }
}