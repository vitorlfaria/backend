using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleProfile class.
/// Tests cover mapping configurations between API requests and application commands.
/// </summary>
public class DeleteSaleProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the DeleteSaleProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeleteSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that a Guid (representing the sale ID) is correctly mapped to a DeleteSaleCommand.
    /// </summary>
    [Fact(DisplayName = "Guid should be mapped to DeleteSaleCommand correctly")]
    public void Given_SaleIdGuid_When_MappedToCommand_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeleteSaleProfile>());
        var mapper = config.CreateMapper();
        var saleId = Guid.NewGuid();

        // Act
        var command = mapper.Map<DeleteSaleCommand>(saleId);

        // Assert
        Assert.Equal(saleId, command.Id);
    }
}