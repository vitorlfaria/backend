using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories;

public class SaleRepositoryTests : IClassFixture<DbContextFixture>
{
    private readonly SaleRepository _repository;
    private readonly DefaultContext _context;

    public SaleRepositoryTests(DbContextFixture fixture)
    {
        _context = fixture.Context;
        _repository = new SaleRepository(_context);
    }

    [Fact(DisplayName = "Should get a sale by number successfully")]
    public async Task GetByNumberAsync_ShouldReturnSale()
    {
        // Arrange
        var saleNumber = 12345;
        var sale = new Sale { Number = saleNumber };
        await _repository.CreateAsync(sale);

        // Act
        var result = await _repository.GetByNumberAsync(saleNumber);

        // Assert
        result.Should().NotBeNull();
        result!.Number.Should().Be(saleNumber);
    }

    [Fact(DisplayName = "Should return null when sale number does not exist")]
    public async Task GetByNumberAsync_ShouldReturnNullWhenNotFound()
    {
        // Arrange
        var nonExistentNumber = 99999;

        // Act
        var result = await _repository.GetByNumberAsync(nonExistentNumber);

        // Assert
        result.Should().BeNull();
    }

    [Theory(DisplayName = "Should throw ArgumentNullException for invalid sale number")]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetByNumberAsync_ShouldThrowExceptionForInvalidNumber(int invalidNumber)
    {
        // Act
        Func<Task> act = async () => await _repository.GetByNumberAsync(invalidNumber);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Number cannot be less than or equal to zero (Parameter 'number')");
    }
}