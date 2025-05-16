using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// AutoMapper profile for mapping between UpdateUserCommand, User, and UpdateUserResult.
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserProfile class.
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore ID during update mapping
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Never update password here
        CreateMap<User, UpdateUserResult>();
    }
}