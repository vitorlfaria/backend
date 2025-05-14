using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator class for Sale entity.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidator"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the validation rules for the Sale entity.
    /// </remarks>
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID cannot be empty.")
            .NotNull()
            .WithMessage("Customer ID cannot be null.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID cannot be empty.")
            .NotNull()
            .WithMessage("Branch ID cannot be null.");

        RuleFor(sale => sale.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero.");

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .WithMessage("Sale items cannot be empty.")
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("All sale items must have a quantity greater than zero.");
    }
}
