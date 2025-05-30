using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IDomainEventsDispatcher domainEventsDispatcher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request.
    /// </summary>
    /// <param name="command">The CreateSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale details.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        foreach (var saleItem in sale.SaleItems)
        {
            var saleItemValidator = new SaleItemValidator();
            var saleItemValidationResult = await saleItemValidator.ValidateAsync(saleItem, cancellationToken);
            if (!saleItemValidationResult.IsValid)
                throw new ValidationException(saleItemValidationResult.Errors);

            saleItem.CalculateTotalPrice();
        }

        sale.CalculateTotals();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        createdSale.Created();
        await _domainEventsDispatcher.DispatchAsync(createdSale.DomainEvents);
        createdSale.ClearDomainEvents();

        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}