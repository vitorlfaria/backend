using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Validator for UpdateBranchCommand.
/// </summary>
public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateBranchCommandValidator class.
    /// </summary>
    public UpdateBranchCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Branch name is required.")
            .Length(1, 100)
            .WithMessage("Branch name must be between 1 and 100 characters.");
    }
}