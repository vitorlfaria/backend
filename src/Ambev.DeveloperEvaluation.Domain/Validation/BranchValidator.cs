using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator class for Branch entity.
/// </summary>
public class BranchValidator : AbstractValidator<Branch>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BranchValidator"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the validation rules for the Branch entity.
    /// </remarks>
    public BranchValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(1, 100)
            .WithMessage("Name must be between 1 and 100 characters.");
    }
}
