using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesResponse and PaginatedListResponse classes.
/// Tests cover response creation and property assignment.
/// </summary>
public class GetPaginatedSalesResponseTests
{
    /// <summary>
    /// Tests that the GetPaginatedSalesResponse is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "GetPaginatedSalesResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var number = 123;
        var saleDate = DateTime.UtcNow;
        var customerId = Guid.NewGuid();
        var customerName = "Test Customer";
        var totalAmount = 100.00m;
        var branchId = Guid.NewGuid();
        var branchName = "Test Branch";
        var canceled = false;

        // Act
        var response = new GetPaginatedSalesResponse
        {
            Id = id,
            Number = number,
            SaleDate = saleDate,
            CustomerId = customerId,
            CustomerName = customerName,
            TotalAmount = totalAmount,
            BranchId = branchId,
            BranchName = branchName,
            Canceled = canceled
        };

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(number, response.Number);
        Assert.Equal(saleDate, response.SaleDate);
        Assert.Equal(customerId, response.CustomerId);
        Assert.Equal(customerName, response.CustomerName);
        Assert.Equal(totalAmount, response.TotalAmount);
        Assert.Equal(branchId, response.BranchId);
        Assert.Equal(branchName, response.BranchName);
        Assert.Equal(canceled, response.Canceled);
    }

    // Additional tests for PaginatedListResponse can be added here
    // For example, testing the properties related to pagination like
    // PageNumber, TotalPages, TotalCount, HasPreviousPage, HasNextPage.
}