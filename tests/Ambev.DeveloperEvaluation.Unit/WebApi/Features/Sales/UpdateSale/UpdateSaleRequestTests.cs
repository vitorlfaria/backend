using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestTests
{
    [Fact(DisplayName = "UpdateSaleRequest should be created with the correct properties")]
    public void Given_Properties_When_RequestCreated_Then_ShouldHaveCorrectProperties()
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
        var saleItems = new List<UpdateSaleItemRequest> { new UpdateSaleItemRequest { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.00m, Discount = 5 } };

        // Act
        var request = new UpdateSaleRequest
        {
            Id = id,
            Number = number,
            SaleDate = saleDate,
            CustomerId = customerId,
            CustomerName = customerName,
            TotalAmount = totalAmount,
            BranchId = branchId,
            BranchName = branchName,
            Canceled = canceled,
            SaleItems = saleItems
        };

        // Assert
        Assert.Equal(id, request.Id);
        Assert.Equal(number, request.Number);
        Assert.Equal(saleDate, request.SaleDate);
        Assert.Equal(customerId, request.CustomerId);
        Assert.Equal(customerName, request.CustomerName);
        Assert.Equal(totalAmount, request.TotalAmount);
        Assert.Equal(branchId, request.BranchId);
        Assert.Equal(branchName, request.BranchName);
        Assert.Equal(canceled, request.Canceled);
        Assert.Equal(saleItems, request.SaleItems);
    }
}