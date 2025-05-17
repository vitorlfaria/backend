using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest that defines validation rules for sale updates.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");

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

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero.");

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

                items.RuleFor(item => item.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero for each sale item.");
            });
    }
}