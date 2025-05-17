using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidatorTests
{
    private readonly UpdateSaleRequestValidator _validator;

    public UpdateSaleRequestValidatorTests()
    {
        _validator = new UpdateSaleRequestValidator();
    }

    [Fact]
    public void Should_have_error_when_Id_is_empty()
    {
        var model = new UpdateSaleRequest { Id = Guid.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.Id)
            .WithErrorMessage("Sale ID is required.");
    }

    [Fact]
    public void Should_not_have_error_when_Id_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid() };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.Id);
    }

    [Fact]
    public void Should_have_error_when_Number_is_zero()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), Number = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.Number)
            .WithErrorMessage("Sale number must be greater than zero.");
    }

    [Fact]
    public void Should_not_have_error_when_Number_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), Number = 123 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.Number);
    }

    [Fact]
    public void Should_have_error_when_SaleDate_is_in_the_future()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), SaleDate = DateTime.UtcNow.AddDays(1) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.SaleDate)
            .WithErrorMessage("Sale date cannot be in the future.");
    }

    [Fact]
    public void Should_not_have_error_when_SaleDate_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), SaleDate = DateTime.UtcNow };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.SaleDate);
    }

    [Fact]
    public void Should_have_error_when_CustomerId_is_empty()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), CustomerId = Guid.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.CustomerId)
            .WithErrorMessage("Customer ID is required.");
    }

    [Fact]
    public void Should_not_have_error_when_CustomerId_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid() };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.CustomerId);
    }

    [Fact]
    public void Should_have_error_when_TotalAmount_is_zero()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), TotalAmount = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.TotalAmount)
            .WithErrorMessage("Total amount must be greater than zero.");
    }

    [Fact]
    public void Should_not_have_error_when_TotalAmount_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), TotalAmount = 100.50m };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.TotalAmount);
    }

    [Fact]
    public void Should_have_error_when_BranchId_is_empty()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), BranchId = Guid.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.BranchId)
            .WithErrorMessage("Branch ID is required.");
    }

    [Fact]
    public void Should_not_have_error_when_BranchId_is_valid()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), BranchId = Guid.NewGuid() };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(sale => sale.BranchId);
    }

    [Fact]
    public void Should_have_error_when_SaleItems_is_empty()
    {
        var model = new UpdateSaleRequest { Id = Guid.NewGuid(), SaleItems = [] };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(sale => sale.SaleItems)
            .WithErrorMessage("Sale items cannot be empty.");
    }

    [Fact]
    public void Should_have_error_when_SaleItems_ProductId_is_empty()
    {
        var model = new UpdateSaleRequest
        {
            Id = Guid.NewGuid(),
            SaleItems = [new UpdateSaleItemRequest { ProductId = Guid.Empty, Quantity = 1 }]
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("SaleItems[0].ProductId")
            .WithErrorMessage("Product ID is required for each sale item.");
    }

    // Adicione mais testes para SaleItems (Quantity, etc.) seguindo o padrão
}