using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Contains unit tests for the CreateSaleProfile class.
/// Tests cover mapping configurations between API requests/responses and application commands/results.
/// </summary>
public class CreateSaleProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the CreateSaleProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that CreateSaleRequest is correctly mapped to CreateSaleCommand.
    /// </summary>
    [Fact(DisplayName = "CreateSaleRequest should be mapped to CreateSaleCommand correctly")]
    public void Given_CreateSaleRequest_When_MappedToCommand_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleProfile>());
        var mapper = config.CreateMapper();
        var request = new CreateSaleRequest { Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), CustomerName = "Test Customer", BranchId = Guid.NewGuid(), BranchName = "Test Branch", SaleItems = [] };

        // Act
        var command = mapper.Map<CreateSaleCommand>(request);

        // Assert
        Assert.Equal(request.Number, command.Number);
        Assert.Equal(request.SaleDate, command.SaleDate);
        Assert.Equal(request.CustomerId, command.CustomerId);
        Assert.Equal(request.CustomerName, command.CustomerName);
        Assert.Equal(request.BranchId, command.BranchId);
        Assert.Equal(request.BranchName, command.BranchName);
        Assert.NotNull(command.SaleItems);
        Assert.Empty(command.SaleItems);
    }
}