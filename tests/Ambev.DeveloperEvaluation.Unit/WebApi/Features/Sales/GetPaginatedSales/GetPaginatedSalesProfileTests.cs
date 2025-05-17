using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesProfile class.
/// Tests cover mapping configurations between API requests/responses and application commands/results.
/// </summary>
public class GetPaginatedSalesProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the GetPaginatedSalesProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales.GetPaginatedSalesProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that GetPaginatedSalesRequest is correctly mapped to GetPaginatedSalesCommand.
    /// </summary>
    [Fact(DisplayName = "GetPaginatedSalesRequest should be mapped to GetPaginatedSalesCommand correctly")]
    public void Given_GetPaginatedSalesRequest_When_MappedToCommand_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales.GetPaginatedSalesProfile>());
        var mapper = config.CreateMapper();
        var request = new GetPaginatedSalesRequest { PageNumber = 2, PageSize = 20 };

        // Act
        var command = mapper.Map<GetPaginatedSalesCommand>(request);

        // Assert
        Assert.Equal(request.PageNumber, command.PageNumber);
        Assert.Equal(request.PageSize, command.PageSize);
    }
}