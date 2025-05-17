using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetSale;

/// <summary>
/// Contains unit tests for the GetSaleProfile class.
/// Tests cover mapping configurations between API requests and application commands.
/// </summary>
public class GetSaleProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the GetSaleProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.GetSale.GetSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that a Guid (representing the sale ID) is correctly mapped to a GetSaleCommand.
    /// </summary>
    [Fact(DisplayName = "Guid should be mapped to GetSaleCommand correctly")]
    public void Given_SaleIdGuid_When_MappedToCommand_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.GetSale.GetSaleProfile>());
        var mapper = config.CreateMapper();
        var saleId = Guid.NewGuid();

        // Act
        var command = mapper.Map<GetSaleCommand>(saleId);

        // Assert
        Assert.Equal(saleId, command.Id);
    }
}