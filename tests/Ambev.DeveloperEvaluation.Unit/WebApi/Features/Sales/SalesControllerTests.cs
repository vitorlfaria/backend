using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales;

/// <summary>
/// Contains unit tests for the SalesController class.
/// </summary>
public class SalesControllerTests
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly SalesController _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalesControllerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public SalesControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _controller = new SalesController(_mediator, _mapper);
    }

    /// <summary>
    /// Tests the CreateSale action with a valid request.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns created response")]
    public async Task CreateSale_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            Number = 123,
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Test Customer",
            TotalAmount = 100.00m,
            BranchId = Guid.NewGuid(),
            BranchName = "Test Branch",
            SaleItems = []
        };
        var command = new CreateSaleCommand();
        var result = new CreateSaleResult { Id = Guid.NewGuid() };
        var response = new CreateSaleResponse { Id = result.Id };

        _mapper.Map<CreateSaleCommand>(request).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<CreateSaleResponse>(result).Returns(response);

        // Act
        var actionResult = await _controller.CreateSale(request, CancellationToken.None);

        // Assert
        var createdResult = actionResult.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        createdResult.Value.Should().Be(response);
    }

    /// <summary>
    /// Tests the GetSale action with a valid ID.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When retrieving sale Then returns sale details")]
    public async Task GetSale_ValidId_ReturnsSaleDetails()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var result = new GetSaleResult { Id = saleId, Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100.00m, BranchId = Guid.NewGuid() };
        var response = new GetSaleResponse { Id = saleId, Number = 123, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 100.00m, BranchId = Guid.NewGuid() };

        _mapper.Map<GetSaleCommand>(saleId).Returns(new GetSaleCommand(saleId));
        _mediator.Send(Arg.Is<GetSaleCommand>(c => c.Id == saleId), Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<GetSaleResponse>(result).Returns(response);

        // Act
        var actionResult = await _controller.GetSale(saleId, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var apiResponse = okResult.Value.Should().BeOfType<ApiResponseWithData<GetSaleResponse>>().Subject;
        apiResponse.Data.Should().Be(response);
    }

    /// <summary>
    /// Tests the GetPaginatedSales action.
    /// </summary>
    [Fact(DisplayName = "When retrieving paginated sales Then returns paginated list")]
    public async Task GetPaginatedSales_ReturnsPaginatedList()
    {
        // Arrange
        var request = new GetPaginatedSalesRequest { PageNumber = 1, PageSize = 10 };
        var command = new GetPaginatedSalesCommand { PageNumber = 1, PageSize = 10 };
        var sales = new List<Sale>
        {
            new Sale { Id = Guid.NewGuid(), Number = 1, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 50.00m, BranchId = Guid.NewGuid() },
            new Sale { Id = Guid.NewGuid(), Number = 2, SaleDate = DateTime.UtcNow, CustomerId = Guid.NewGuid(), TotalAmount = 75.00m, BranchId = Guid.NewGuid() }
        };
        var result = new PaginatedList<GetPaginatedSalesResult>(sales.Select(s => new GetPaginatedSalesResult { Id = s.Id, Number = s.Number, SaleDate = s.SaleDate, CustomerId = s.CustomerId, TotalAmount = s.TotalAmount, BranchId = s.BranchId }).ToList(), 2, 1, 10);
        var response = new PaginatedListResponse<GetPaginatedSalesResponse>
        {
            Items = result.Select(s => new GetPaginatedSalesResponse { Id = s.Id, Number = s.Number, SaleDate = s.SaleDate, CustomerId = s.CustomerId, TotalAmount = s.TotalAmount, BranchId = s.BranchId }).ToList(),
            PageNumber = 1,
            TotalPages = 1,
            TotalCount = 2
        };

        _mapper.Map<GetPaginatedSalesCommand>(request).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<PaginatedListResponse<GetPaginatedSalesResponse>>(result).Returns(response);

        // Act
        var actionResult = await _controller.GetPaginatedSales(request, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var paginatedListResponse = okResult.Value.Should().BeOfType<ApiResponseWithData<PaginatedListResponse<GetPaginatedSalesResponse>>>().Subject;
        paginatedListResponse.Data.Should().BeEquivalentTo(response);
    }

    /// <summary>
    /// Tests the DeleteSale action with a valid ID.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When deleting sale Then returns success response")]
    public async Task DeleteSale_ValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSaleCommand(saleId);
        var result = new DeveloperEvaluation.Application.Sales.DeleteSale.DeleteSaleResponse { Success = true };

        _mapper.Map<DeleteSaleCommand>(saleId).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);

        // Act
        var actionResult = await _controller.DeleteSale(saleId, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var response = okResult.Value.Should().BeOfType<ApiResponseWithData<DeveloperEvaluation.Application.Sales.DeleteSale.DeleteSaleResponse>>().Subject;
        response.Success.Should().BeTrue();
        response.Should().NotBeNull();
    }

    /// <summary>
    /// Tests the DeleteSale action with a non-existent ID.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When deleting sale Then returns not found")]
    public async Task DeleteSale_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSaleCommand(saleId);

        _mapper.Map<DeleteSaleCommand>(saleId).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(new DeveloperEvaluation.Application.Sales.DeleteSale.DeleteSaleResponse { Success = false });

        // Act
        var actionResult = await _controller.DeleteSale(saleId, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var response = okResult.Value.Should().BeOfType<ApiResponseWithData<DeveloperEvaluation.Application.Sales.DeleteSale.DeleteSaleResponse>>().Subject;
        response.Data.Success.Should().BeFalse();
        response.Should().NotBeNull();
    }
}