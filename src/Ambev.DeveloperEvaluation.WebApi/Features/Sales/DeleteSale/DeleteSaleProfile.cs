using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Profile for mapping between API requests and application commands for DeleteSale.
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Configures the mappings for DeleteSale operation.
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, DeleteSaleCommand>()
            .ConvertUsing(id => new DeleteSaleCommand(id));

        CreateMap<DeleteSaleResponse, DeleteSaleResponse>()
            .ConvertUsing(response => new DeleteSaleResponse(response.Success));
    }
}