using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator class.
    /// </summary>
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .Length(1, 100)
            .WithMessage("Product name must be between 1 and 100 characters.");

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Product description is required.")
            .Length(1, 500)
            .WithMessage("Product description must be between 1 and 500 characters.");

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");
    }
}