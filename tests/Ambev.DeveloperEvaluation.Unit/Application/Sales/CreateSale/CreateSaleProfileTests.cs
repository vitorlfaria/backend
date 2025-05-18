using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Xunit;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.CreateSale;

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
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that CreateSaleCommand is correctly mapped to Sale.
    /// </summary>
    [Fact(DisplayName = "CreateSaleCommand should be mapped to Sale correctly")]
    public void Given_CreateSaleCommand_When_MappedToSale_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>());
        var mapper = config.CreateMapper();
        var command = new CreateSaleCommand
        {
            Number = 12345,
            SaleDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleItems = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.25m, Discount = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.00m, Discount = 5 }
            },
            Canceled = false
        };

        // Act
        var sale = mapper.Map<Sale>(command);

        // Assert
        Assert.Equal(command.Number, sale.Number);
        Assert.Equal(command.SaleDate, sale.SaleDate);
        Assert.Equal(command.CustomerId, sale.CustomerId);
        Assert.Equal(command.BranchId, sale.BranchId);
        Assert.Equal(command.Canceled, sale.Canceled);
        Assert.Equal(command.SaleItems.Count, sale.SaleItems.Count);
    }
}