namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Represents the result of a create branch operation.
/// </summary>
public class CreateBranchResult
{
    /// <summary>
    /// Gets or sets the ID of the created branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the created branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}