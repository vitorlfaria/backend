using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetPaginatedSales;

/// <summary>
/// Contains unit tests for the <see cref="GetPaginatedSalesHandler"/> class.
/// </summary>
public class GetPaginatedSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetPaginatedSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaginatedSalesHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetPaginatedSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetPaginatedSalesHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a paginated list of sales is retrieved successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid pagination parameters When retrieving sales Then returns paginated list")]
    public async Task Handle_ValidRequest_ReturnsPaginatedList()
    {
        // Given
        var pageNumber = 1;
        var pageSize = 10;
        var command = new GetPaginatedSalesCommand { PageNumber = pageNumber, PageSize = pageSize };
        var sales = new List<Sale>
        {
            new() { Id = Guid.NewGuid(), Number = 1, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100, BranchId = Guid.NewGuid(), Canceled = false },
            new() { Id = Guid.NewGuid(), Number = 2, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 200, BranchId = Guid.NewGuid(), Canceled = false }
        };
        var paginatedSales = new PaginatedList<Sale>(sales, sales.Count, pageNumber, pageSize);
        var results = sales.Select(s => new GetPaginatedSalesResult { Id = s.Id, Number = s.Number, SaleDate = s.SaleDate, CustomerId = s.CustomerId, TotalAmount = s.TotalAmount, BranchId = s.BranchId, Canceled = s.Canceled }).ToList();

        _saleRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedSales);
        _mapper.Map<GetPaginatedSalesResult>(Arg.Any<Sale>()).ReturnsForAnyArgs(x => results.First(r => r.Id == ((Sale)x[0]).Id));

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(sales.Count);
        result.CurrentPage.Should().Be(pageNumber);
        result.PageSize.Should().Be(pageSize);
        await _saleRepository.Received(1).GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the sale repository is called with the correct pagination parameters.
    /// </summary>
    [Fact(DisplayName = "Given pagination parameters When handling Then calls repository with correct parameters")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectParameters()
    {
        // Given
        var pageNumber = 2;
        var pageSize = 5;
        var command = new GetPaginatedSalesCommand { PageNumber = pageNumber, PageSize = pageSize };
        var sales = new List<Sale>();
        var paginatedSales = new PaginatedList<Sale>(sales, 0, pageNumber, pageSize);

        _saleRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedSales);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).GetPaginatedAsync(Arg.Is(pageNumber), Arg.Is(pageSize), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called for each sale in the paginated list.
    /// </summary>
    [Fact(DisplayName = "Given paginated sales When handling Then maps each sale to result")]
    public async Task Handle_PaginatedSales_MapsEachSaleToResult()
    {
        // Given
        var pageNumber = 1;
        var pageSize = 10;
        var command = new GetPaginatedSalesCommand { PageNumber = pageNumber, PageSize = pageSize };
        var sales = new List<Sale>
        {
            new Sale { Id = Guid.NewGuid(), Number = 1, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100, BranchId = Guid.NewGuid(), Canceled = false },
            new Sale { Id = Guid.NewGuid(), Number = 2, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 200, BranchId = Guid.NewGuid(), Canceled = false },
            new Sale { Id = Guid.NewGuid(), Number = 3, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 300, BranchId = Guid.NewGuid(), Canceled = false }
        };
        var paginatedSales = new PaginatedList<Sale>(sales, sales.Count, pageNumber, pageSize);
        var results = sales.Select(s => new GetPaginatedSalesResult { Id = s.Id, Number = s.Number, SaleDate = s.SaleDate, CustomerId = s.CustomerId, TotalAmount = s.TotalAmount, BranchId = s.BranchId, Canceled = s.Canceled }).ToList();

        _saleRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedSales);
        _mapper.Map<GetPaginatedSalesResult>(Arg.Any<Sale>()).ReturnsForAnyArgs(x => results.First(r => r.Id == ((Sale)x[0]).Id));

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        foreach (var sale in sales)
        {
            _mapper.Received(1).Map<GetPaginatedSalesResult>(Arg.Is<Sale>(s => s.Id == sale.Id));
        }
    }
}