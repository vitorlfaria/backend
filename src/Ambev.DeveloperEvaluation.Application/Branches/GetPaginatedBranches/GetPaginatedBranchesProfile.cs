using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetPaginatedBranches;

/// <summary>
/// AutoMapper profile for mapping between Branch entity and GetPaginatedBranchesResult.
/// </summary>
public class GetPaginatedBranchesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedBranches operation.
    /// </summary>
    public GetPaginatedBranchesProfile()
    {
        CreateMap<Branch, GetPaginatedBranchesResult>();
    }
}