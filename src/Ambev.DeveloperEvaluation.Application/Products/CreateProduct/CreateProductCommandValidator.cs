using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator class.
    /// </summary>
    public CreateProductCommandValidator()
    {
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