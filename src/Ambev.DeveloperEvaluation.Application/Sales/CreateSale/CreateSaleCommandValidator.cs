using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator class.
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(command => command.Number)
            .GreaterThan(0)
            .WithMessage("Sale number must be greater than zero.");

        RuleFor(command => command.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        RuleFor(command => command.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(command => command.SaleItems)
            .NotEmpty()
            .WithMessage("Sale items cannot be empty.");

        RuleForEach(command => command.SaleItems)
            .ChildRules(items =>
            {
                items.RuleFor(item => item.ProductId)
                    .NotEmpty()
                    .WithMessage("Product ID is required for each sale item.");

                items.RuleFor(item => item.ProductName)
                    .NotEmpty()
                    .WithMessage("Product name is required for each sale item.");

                items.RuleFor(item => item.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero for each sale item.");

                items.RuleFor(item => item.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage("Unit price must be greater than zero for each sale item.");

                items.RuleFor(item => item.Discount)
                    .InclusiveBetween(0, 100)
                    .WithMessage("Discount must be between 0 and 100 for each sale item.");
            });
    }
}