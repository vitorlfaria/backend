using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IDomainEventsDispatcher domainEventsDispatcher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request.
    /// </summary>
    /// <param name="command">The UpdateSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated sale details.</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        _mapper.Map(command, sale);

        foreach (var saleItem in sale.SaleItems)
        {
            saleItem.CalculateTotalPrice();
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        sale.Modify();
        await _domainEventsDispatcher.DispatchAsync(sale.DomainEvents);
        sale.ClearDomainEvents();

        var result = _mapper.Map<UpdateSaleResult>(sale);
        return result;
    }
}