using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetPaginatedBranches;

/// <summary>
/// Command to retrieve a paginated list of branches.
/// </summary>
public record GetPaginatedBranchesCommand : IRequest<PaginatedList<GetPaginatedBranchesResult>>
{
    /// <summary>
    /// The page number to retrieve.
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; init; } = 10;
}