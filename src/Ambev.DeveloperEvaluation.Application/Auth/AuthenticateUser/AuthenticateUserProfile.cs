using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

/// <summary>
/// AutoMapper profile for authentication-related mappings
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
    /// </summary>
    public AuthenticateUserProfile()
    {
        CreateMap<User, AuthenticateUserResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
