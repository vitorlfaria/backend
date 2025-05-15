using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Products.GetPaginatedProducts;

/// <summary>
/// Handler for processing GetPaginatedProductsCommand requests.
/// </summary>
public class GetPaginatedProductsHandler : IRequestHandler<GetPaginatedProductsCommand, PaginatedList<GetPaginatedProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetPaginatedProductsHandler.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetPaginatedProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetPaginatedProductsCommand request.
    /// </summary>
    /// <param name="request">The GetPaginatedProducts command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of product details.</returns>
    public async Task<PaginatedList<GetPaginatedProductsResult>> Handle(GetPaginatedProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = products.ConvertAll(product => _mapper.Map<GetPaginatedProductsResult>(product));
        return new PaginatedList<GetPaginatedProductsResult>(result, products.Count, request.PageNumber, request.PageSize);
    }
}