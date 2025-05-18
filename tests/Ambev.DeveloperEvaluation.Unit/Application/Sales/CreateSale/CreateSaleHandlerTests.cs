using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.CreateSale;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new CreateSaleCommand
        {
            Number = 12345,
            SaleDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleItems = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), ProductName = "Product 1", Quantity = 2, UnitPrice = 25.25m, Discount = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), ProductName = "Product 2", Quantity = 1, UnitPrice = 50.00m, Discount = 5 }
            },
            Canceled = false
        };
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Number = command.Number,
            SaleDate = command.SaleDate,
            CustomerId = command.CustomerId,
            BranchId = command.BranchId,
            SaleItems = command.SaleItems.ConvertAll(item => new SaleItem { ProductId = item.ProductId, ProductName = item.ProductName, Quantity = item.Quantity, UnitPrice = item.UnitPrice, Discount = item.Discount }),
            Canceled = command.Canceled
        };
        sale.CalculateTotals();
        var result = new CreateSaleResult { Id = sale.Id, Number = sale.Number, SaleDate = sale.SaleDate, CustomerId = sale.CustomerId, TotalAmount = sale.TotalAmount, BranchId = sale.BranchId, Canceled = sale.Canceled };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        createSaleResult.Number.Should().Be(sale.Number);
        createSaleResult.SaleDate.Should().Be(sale.SaleDate);
        createSaleResult.CustomerId.Should().Be(sale.CustomerId);
        createSaleResult.BranchId.Should().Be(sale.BranchId);
        createSaleResult.Canceled.Should().Be(sale.Canceled);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Invalid: empty command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        // Given
        var command = new CreateSaleCommand { Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), BranchId = Guid.NewGuid(), SaleItems = [new SaleItemDto(){ ProductId = Guid.NewGuid(), ProductName = "Product 1", Quantity = 1, UnitPrice = 50, Discount = 10 }], Canceled = false };
        var sale = new Sale();

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c => c == command));
    }

    /// <summary>
    /// Tests that the sale repository is called to create the sale.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When handling Then creates sale in repository")]
    public async Task Handle_ValidSale_CreatesSaleInRepository()
    {
        // Given
        var command = new CreateSaleCommand { Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), BranchId = Guid.NewGuid(), SaleItems = [new SaleItemDto(){ ProductId = Guid.NewGuid(), ProductName = "Product 1", Quantity = 1, UnitPrice = 50, Discount = 10 }], Canceled = false };
        var sale = new Sale { Id = Guid.NewGuid(), Number = command.Number, SaleDate = command.SaleDate, CustomerId = command.CustomerId, TotalAmount = 1, BranchId = command.BranchId, SaleItems = command.SaleItems.ConvertAll(item => new SaleItem { ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = item.UnitPrice, Discount = item.Discount }), Canceled = command.Canceled };
        _mapper.Map<Sale>(command).Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}