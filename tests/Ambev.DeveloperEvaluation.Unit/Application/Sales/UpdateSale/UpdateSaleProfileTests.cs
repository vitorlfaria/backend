using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Xunit;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.UpdateSale;

/// <summary>
/// Contains unit tests for the UpdateSaleProfile class.
/// Tests cover mapping configurations between commands, entities, and results.
/// </summary>
public class UpdateSaleProfileTests
{
    /// <summary>
    /// Tests that the AutoMapper configuration is valid for the UpdateSaleProfile.
    /// </summary>
    [Fact(DisplayName = "AutoMapper configuration should be valid")]
    public void Given_MappingProfile_When_ConfigurationChecked_Then_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UpdateSaleProfile>());

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Tests that UpdateSaleCommand is correctly mapped to Sale.
    /// </summary>
    [Fact(DisplayName = "UpdateSaleCommand should be mapped to Sale correctly")]
    public void Given_UpdateSaleCommand_When_MappedToSale_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UpdateSaleProfile>());
        var mapper = config.CreateMapper();
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            Number = 54321,
            SaleDate = DateTime.UtcNow.AddDays(-2),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 150.75m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.50m, Discount = 5 },
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.12m, Discount = 2 }
            },
            Canceled = true
        };

        // Act
        var sale = mapper.Map<Sale>(command);

        // Assert
        Assert.Equal(command.Number, sale.Number);
        Assert.Equal(command.SaleDate, sale.SaleDate);
        Assert.Equal(command.CustomerId, sale.CustomerId);
        Assert.Equal(command.TotalAmount, sale.TotalAmount);
        Assert.Equal(command.BranchId, sale.BranchId);
        Assert.Equal(command.Canceled, sale.Canceled);
        Assert.Equal(command.SaleItems.Count, sale.SaleItems.Count);
    }
}