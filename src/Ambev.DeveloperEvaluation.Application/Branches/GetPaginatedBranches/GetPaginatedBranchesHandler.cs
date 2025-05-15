using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetPaginatedBranches;

/// <summary>
/// Handler for processing GetPaginatedBranchesCommand requests.
/// </summary>
public class GetPaginatedBranchesHandler : IRequestHandler<GetPaginatedBranchesCommand, PaginatedList<GetPaginatedBranchesResult>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetPaginatedBranchesHandler.
    /// </summary>
    /// <param name="branchRepository">The branch repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetPaginatedBranchesHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetPaginatedBranchesCommand request.
    /// </summary>
    /// <param name="request">The GetPaginatedBranches command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of branch details.</returns>
    public async Task<PaginatedList<GetPaginatedBranchesResult>> Handle(GetPaginatedBranchesCommand request, CancellationToken cancellationToken)
    {
        var branches = await _branchRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = branches.ConvertAll(branch => _mapper.Map<GetPaginatedBranchesResult>(branch));
        return new PaginatedList<GetPaginatedBranchesResult>(result, branches.Count, request.PageNumber, request.PageSize);
    }
}