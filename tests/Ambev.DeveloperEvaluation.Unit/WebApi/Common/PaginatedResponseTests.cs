using Ambev.DeveloperEvaluation.WebApi.Common;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Common;

/// <summary>
/// Contains unit tests for the PaginatedResponse class.
/// Tests cover response creation and property assignment, including pagination details.
/// </summary>
public class PaginatedResponseTests
{
    /// <summary>
    /// Tests that the PaginatedResponse is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "PaginatedResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var success = true;
        var message = "Test message";
        var data = new List<int> { 1, 2, 3 };
        var currentPage = 2;
        var totalPages = 4;
        var totalCount = 10;

        // Act
        var response = new PaginatedResponse<int>
        {
            Success = success,
            Message = message,
            Data = data,
            CurrentPage = currentPage,
            TotalPages = totalPages,
            TotalCount = totalCount
        };

        // Assert
        Assert.Equal(success, response.Success);
        Assert.Equal(message, response.Message);
        Assert.Equal(data, response.Data);
        Assert.Equal(currentPage, response.CurrentPage);
        Assert.Equal(totalPages, response.TotalPages);
        Assert.Equal(totalCount, response.TotalCount);
    }
}