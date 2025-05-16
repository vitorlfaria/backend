using Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetPaginatedUsers;

/// <summary>
/// Contains unit tests for the GetPaginatedUsersResult class.
/// Tests cover property initialization.
/// </summary>
public class GetPaginatedUsersResultTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly")]
    public void Given_GetPaginatedUsersResult_When_IdIsSet_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = new GetPaginatedUsersResult { Id = id };

        // Assert
        Assert.Equal(id, result.Id);
    }

    /// <summary>
    /// Tests that the Name property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Name property should be initialized correctly")]
    public void Given_GetPaginatedUsersResult_When_NameIsSet_Then_NameShouldBeCorrect()
    {
        // Arrange
        var name = "Test User";

        // Act
        var result = new GetPaginatedUsersResult { Name = name };

        // Assert
        Assert.Equal(name, result.Name);
    }

    /// <summary>
    /// Tests that the Email property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Email property should be initialized correctly")]
    public void Given_GetPaginatedUsersResult_When_EmailIsSet_Then_EmailShouldBeCorrect()
    {
        // Arrange
        var email = "test@example.com";

        // Act
        var result = new GetPaginatedUsersResult { Email = email };

        // Assert
        Assert.Equal(email, result.Email);
    }
}