using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetSale;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale retrieval request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When retrieving sale Then returns sale details")]
    public async Task Handle_ValidRequest_ReturnsSaleDetails()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);
        var sale = new Sale { Id = saleId, Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100, BranchId = Guid.NewGuid(), Canceled = false };
        var result = new GetSaleResult { Id = saleId, Number = 123, SaleDate = sale.SaleDate, CustomerId = sale.CustomerId, TotalAmount = 100, BranchId = sale.BranchId, Canceled = false };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(result);

        // When
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(saleId);
        getSaleResult.Number.Should().Be(123);
        getSaleResult.SaleDate.Date.Should().Be(sale.SaleDate.Date);
        getSaleResult.CustomerId.Should().Be(sale.CustomerId);
        getSaleResult.TotalAmount.Should().Be(100);
        getSaleResult.BranchId.Should().Be(sale.BranchId);
        getSaleResult.Canceled.Should().BeFalse();
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale retrieval request (empty ID) throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When retrieving sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetSaleCommand(Guid.Empty); // Invalid: empty ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that a request to retrieve a non-existent sale throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When retrieving sale Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {saleId} not found");
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the sale repository is called with the correct sale ID.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When handling Then calls repository with correct ID")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectId()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);
        var sale = new Sale { Id = saleId, Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100, BranchId = Guid.NewGuid(), Canceled = false };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).GetByIdAsync(Arg.Is(saleId), Arg.Any<CancellationToken>());
    }
}