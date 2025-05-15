using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// AutoMapper profile for mapping between UpdateBranchCommand, Branch, and UpdateBranchResult.
/// </summary>
public class UpdateBranchProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UpdateBranchProfile class.
    /// </summary>
    public UpdateBranchProfile()
    {
        CreateMap<UpdateBranchCommand, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Branch, UpdateBranchResult>();
    }
}