using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator class for Branch entity.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BranchValidator"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the validation rules for the Branch entity.
    /// </remarks>
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(1, 100)
            .WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .Length(1, 500)
            .WithMessage("Description must be between 1 and 500 characters.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required.")
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}
