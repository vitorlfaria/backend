using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the GetPaginatedSalesProfile class.
/// Tests cover mapping configurations between entities and results.
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
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GetPaginatedSalesProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that Sale is correctly mapped to GetPaginatedSalesResult.
    /// </summary>
    [Fact(DisplayName = "Sale should be mapped to GetPaginatedSalesResult correctly")]
    public void Given_SaleEntity_When_MappedToGetPaginatedSalesResult_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetPaginatedSalesProfile>());
        var mapper = config.CreateMapper();
        var sale = new Sale { Id = Guid.NewGuid(), Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 150.00m, BranchId = Guid.NewGuid(), Canceled = false };

        // Act
        var result = mapper.Map<GetPaginatedSalesResult>(sale);

        // Assert
        Assert.Equal(sale.Id, result.Id);
        Assert.Equal(sale.Number, result.Number);
        Assert.Equal(sale.SaleDate, result.SaleDate);
        Assert.Equal(sale.CustomerId, result.CustomerId);
        Assert.Equal(sale.TotalAmount, result.TotalAmount);
        Assert.Equal(sale.BranchId, result.BranchId);
        Assert.Equal(sale.Canceled, result.Canceled);
    }
}