using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class GetPaginatedSalesRequestTests
{
    /// <summary>
    /// Tests that the GetPaginatedSalesRequest is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "GetPaginatedSalesRequest should be created with the correct properties")]
    public void Given_Properties_When_RequestCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var pageNumber = 2;
        var pageSize = 20;

        // Act
        var request = new GetPaginatedSalesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Assert
        Assert.Equal(pageNumber, request.PageNumber);
        Assert.Equal(pageSize, request.PageSize);
    }
}