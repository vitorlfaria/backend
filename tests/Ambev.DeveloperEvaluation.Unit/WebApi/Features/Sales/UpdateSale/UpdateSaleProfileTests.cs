using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleProfileTests
{
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.UpdateSale.UpdateSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "UpdateSaleRequest should be mapped to UpdateSaleCommand correctly")]
    public void Given_UpdateSaleRequest_When_MappedToCommand_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.UpdateSale.UpdateSaleProfile>());
        var mapper = config.CreateMapper();
        var request = new UpdateSaleRequest
        {
            Id = Guid.NewGuid(),
            Number = 456,
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Updated Customer",
            TotalAmount = 150.00m,
            BranchId = Guid.NewGuid(),
            BranchName = "Updated Branch",
            Canceled = true,
            SaleItems = [new UpdateSaleItemRequest { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.00m, Discount = 5 }]
        };

        // Act
        var command = mapper.Map<UpdateSaleCommand>(request);

        // Assert
        Assert.Equal(request.Id, command.Id);
        Assert.Equal(request.Number, command.Number);
        Assert.Equal(request.SaleDate, command.SaleDate);
        Assert.Equal(request.CustomerId, command.CustomerId);
        Assert.Equal(request.CustomerName, command.CustomerName);
        Assert.Equal(request.TotalAmount, command.TotalAmount);
        Assert.Equal(request.BranchId, command.BranchId);
        Assert.Equal(request.BranchName, command.BranchName);
        Assert.Equal(request.Canceled, command.Canceled);
        Assert.NotNull(command.SaleItems);
        Assert.Single(command.SaleItems);
    }
}