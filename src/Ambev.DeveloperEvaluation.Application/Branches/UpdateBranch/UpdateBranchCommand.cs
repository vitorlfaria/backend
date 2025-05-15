using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Command for updating an existing branch.
/// </summary>
public class UpdateBranchCommand : IRequest<UpdateBranchResult>
{
    /// <summary>
    /// Gets or sets the ID of the branch to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the new name for the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Validates the UpdateBranchCommand using UpdateBranchCommandValidator.
    /// </summary>
    /// <returns>A ValidationResultDetail containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateBranchCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}