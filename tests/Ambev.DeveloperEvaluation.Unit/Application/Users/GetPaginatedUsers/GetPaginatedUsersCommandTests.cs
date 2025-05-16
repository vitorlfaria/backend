using Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetPaginatedUsers;

/// <summary>
/// Contains unit tests for the GetPaginatedUsersCommand class.
/// Tests cover initialization and default values.
/// </summary>
public class GetPaginatedUsersCommandTests
{
    /// <summary>
    /// Tests that the PageNumber property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "PageNumber property should be initialized correctly")]
    public void Given_GetPaginatedUsersCommand_When_PageNumberIsSet_Then_PageNumberShouldBeCorrect()
    {
        // Arrange
        var pageNumber = 2;

        // Act
        var command = new GetPaginatedUsersCommand { PageNumber = pageNumber };

        // Assert
        Assert.Equal(pageNumber, command.PageNumber);
    }

    /// <summary>
    /// Tests that the PageSize property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "PageSize property should be initialized correctly")]
    public void Given_GetPaginatedUsersCommand_When_PageSizeIsSet_Then_PageSizeShouldBeCorrect()
    {
        // Arrange
        var pageSize = 20;

        // Act
        var command = new GetPaginatedUsersCommand { PageSize = pageSize };

        // Assert
        Assert.Equal(pageSize, command.PageSize);
    }
}