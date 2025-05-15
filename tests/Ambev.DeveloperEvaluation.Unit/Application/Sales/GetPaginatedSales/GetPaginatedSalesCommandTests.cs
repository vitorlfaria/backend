using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesCommand class.
/// Tests cover initialization and default values.
/// </summary>
public class GetPaginatedSalesCommandTests
{
    /// <summary>
    /// Tests that the PageNumber property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "PageNumber property should be initialized correctly")]
    public void Given_GetPaginatedSalesCommand_When_PageNumberIsSet_Then_PageNumberShouldBeCorrect()
    {
        // Arrange
        var pageNumber = 2;

        // Act
        var command = new GetPaginatedSalesCommand { PageNumber = pageNumber };

        // Assert
        Assert.Equal(pageNumber, command.PageNumber);
    }

    /// <summary>
    /// Tests that the PageSize property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "PageSize property should be initialized correctly")]
    public void Given_GetPaginatedSalesCommand_When_PageSizeIsSet_Then_PageSizeShouldBeCorrect()
    {
        // Arrange
        var pageSize = 20;

        // Act
        var command = new GetPaginatedSalesCommand { PageSize = pageSize };

        // Assert
        Assert.Equal(pageSize, command.PageSize);
    }

    // You can add more tests here if GetPaginatedSalesCommand has other properties
    // that need to be tested for correct initialization or validation.
    // For example, if you add sorting or filtering parameters.
}