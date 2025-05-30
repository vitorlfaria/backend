using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleResponseTests
{
    [Fact(DisplayName = "UpdateSaleResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var number = 456;
        var saleDate = DateTime.UtcNow;
        var customerId = Guid.NewGuid();
        var customerName = "Updated Customer";
        var totalAmount = 150.00m;
        var branchId = Guid.NewGuid();
        var branchName = "Updated Branch";
        var canceled = true;

        // Act
        var response = new UpdateSaleResponse
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
}