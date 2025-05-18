using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.Number)
            .GreaterThan(0)
            .WithMessage("Sale number must be greater than zero.");

        RuleFor(sale => sale.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(5))
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .WithMessage("Sale items cannot be empty.");

        RuleForEach(sale => sale.SaleItems)
            .ChildRules(items =>
            {
                items.RuleFor(item => item.ProductId)
                    .NotEmpty()
                    .WithMessage("Product ID is required for each sale item.");

                items.RuleFor(item => item.ProductName)
                    .NotEmpty()
                    .WithMessage("Product ID is required for each sale item.");

                items.RuleFor(item => item.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero for each sale item.");

                items.RuleFor(item => item.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage("Unit price must be greater than zero for each sale item.");
            });
    }
}