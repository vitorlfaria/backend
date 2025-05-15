using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.UpdateSale;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale update request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            Number = 54321,
            SaleDate = DateTime.UtcNow.AddDays(-2),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 150.75m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.50m, Discount = 5 },
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.12m, Discount = 2 }
            },
            Canceled = true
        };
        var sale = new Sale
        {
            Id = saleId,
            Number = 12345,
            SaleDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            BranchId = Guid.NewGuid(),
            SaleItems = new List<SaleItem>(),
            Canceled = false
        };
        var result = new UpdateSaleResult { Id = saleId, Number = command.Number, SaleDate = command.SaleDate, CustomerId = command.CustomerId, TotalAmount = command.TotalAmount, BranchId = command.BranchId, Canceled = command.Canceled };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map(command, sale);
        _mapper.Map<UpdateSaleResult>(sale).Returns(result);

        // When
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateSaleResult.Should().NotBeNull();
        updateSaleResult.Id.Should().Be(saleId);
        updateSaleResult.Number.Should().Be(command.Number);
        updateSaleResult.SaleDate.Should().Be(command.SaleDate);
        updateSaleResult.CustomerId.Should().Be(command.CustomerId);
        updateSaleResult.TotalAmount.Should().Be(command.TotalAmount);
        updateSaleResult.BranchId.Should().Be(command.BranchId);
        updateSaleResult.Canceled.Should().Be(command.Canceled);
        await _saleRepository.Received(1).UpdateAsync(Arg.Is(sale), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateSaleCommand(); // Invalid: empty command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that a request to update a non-existent sale throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When updating sale Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            Number = 54321,
            SaleDate = DateTime.UtcNow.AddDays(-2),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 150.75m,
            BranchId = Guid.NewGuid(),
            SaleItems =
            [
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 30.50m, Discount = 5 },
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 25.12m, Discount = 2 }
            ],
            Canceled = false
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {saleId} not found");
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
    }

    // You can add more tests here, for example:
    // - Test if the handler correctly handles exceptions thrown by the repository.
    // - Test if the handler returns the expected response when the repository operation fails.
    // [Fact(DisplayName = "Given repository throws exception When handling Then rethrows exception")]
    // public async Task Handle_RepositoryThrowsException_Then_RethrowsException()
    // {
    //     // Arrange & Act & Assert
    // }
}