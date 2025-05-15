namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Represents the result of an update branch operation.
/// </summary>
public class UpdateBranchResult
{
    /// <summary>
    /// Gets or sets the ID of the updated branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the updated branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}