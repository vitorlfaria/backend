using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// AutoMapper profile for mapping between CreateBranchCommand, Branch, and CreateBranchResult.
/// </summary>
public class CreateBranchProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchProfile class.
    /// </summary>
    public CreateBranchProfile()
    {
        CreateMap<Branch, CreateBranchCommand>().ReverseMap();
        CreateMap<Branch, CreateBranchResult>().ReverseMap();
    }
}