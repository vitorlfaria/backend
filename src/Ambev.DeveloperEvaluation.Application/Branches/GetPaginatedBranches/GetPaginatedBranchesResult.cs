namespace Ambev.DeveloperEvaluation.Application.Branches.GetPaginatedBranches;

/// <summary>
/// Represents a branch in a paginated list.
/// </summary>
public class GetPaginatedBranchesResult
{
    /// <summary>
    /// The unique identifier of the branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}