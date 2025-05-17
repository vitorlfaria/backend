using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;

/// <summary>
/// Handler for processing GetPaginatedSalesCommand requests.
/// </summary>
public class GetPaginatedSalesHandler : IRequestHandler<GetPaginatedSalesCommand, PaginatedList<GetPaginatedSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetPaginatedSalesHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetPaginatedSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetPaginatedSalesCommand request.
    /// </summary>
    /// <param name="request">The GetPaginatedSales command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of sale details.</returns>
    public async Task<PaginatedList<GetPaginatedSalesResult>> Handle(GetPaginatedSalesCommand request, CancellationToken cancellationToken)
    {
        var totalCount = await _saleRepository.CountAsync(cancellationToken);
        var sales = await _saleRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = sales.ConvertAll(sale => _mapper.Map<GetPaginatedSalesResult>(sale));
        return new PaginatedList<GetPaginatedSalesResult>(result, totalCount, request.PageNumber, request.PageSize);
    }
}