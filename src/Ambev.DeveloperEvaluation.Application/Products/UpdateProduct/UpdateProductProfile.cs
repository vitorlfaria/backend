using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// AutoMapper profile for mapping between UpdateProductCommand, Product, and UpdateProductResult.
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductProfile class.
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID during update mapping
        CreateMap<Product, UpdateProductResult>();
    }
}