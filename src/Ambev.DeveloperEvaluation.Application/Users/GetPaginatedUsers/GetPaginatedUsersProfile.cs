using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;

/// <summary>
/// AutoMapper profile for mapping between User entity and GetPaginatedUsersResult.
/// </summary>
public class GetPaginatedUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetPaginatedUsers operation.
    /// </summary>
    public GetPaginatedUsersProfile()
    {
        CreateMap<User, GetPaginatedUsersResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
    }
}