using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;

/// <summary>
/// Handler for processing GetPaginatedUsersCommand requests.
/// </summary>
public class GetPaginatedUsersHandler : IRequestHandler<GetPaginatedUsersCommand, PaginatedList<GetPaginatedUsersResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetPaginatedUsersHandler.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetPaginatedUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetPaginatedUsersCommand request.
    /// </summary>
    /// <param name="request">The GetPaginatedUsers command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of user details.</returns>
    public async Task<PaginatedList<GetPaginatedUsersResult>> Handle(GetPaginatedUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = users.ConvertAll(user => _mapper.Map<GetPaginatedUsersResult>(user));
        return new PaginatedList<GetPaginatedUsersResult>(result, users.Count, request.PageNumber, request.PageSize);
    }
}