using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository, IDomainEventsDispatcher domainEventsDispatcher)
    {
        _saleRepository = saleRepository;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request.
    /// </summary>
    /// <param name="request">The DeleteSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        await _saleRepository.UpdateAsync(sale, cancellationToken);
        sale.Cancel();
        await _domainEventsDispatcher.DispatchAsync(sale.DomainEvents);
        sale.ClearDomainEvents();

        return new DeleteSaleResponse { Success = true };
    }
}