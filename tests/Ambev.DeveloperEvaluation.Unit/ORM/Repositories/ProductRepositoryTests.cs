using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories;

public class ProductRepositoryTests : IClassFixture<DbContextFixture>
{
    private readonly ProductRepository _repository;
    private readonly DefaultContext _context;

    public ProductRepositoryTests(DbContextFixture fixture)
    {
        _context = fixture.Context;
        _repository = new ProductRepository(_context);
    }

    [Fact(DisplayName = "Should get a product by name successfully")]
    public async Task GetByNameAsync_ShouldReturnProduct()
    {
        // Arrange
        var productName = "Test Product";
        var product = new Product { Name = productName };
        await _repository.CreateAsync(product);

        // Act
        var result = await _repository.GetByNameAsync(productName);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Contain(productName);
    }

    [Fact(DisplayName = "Should return null when product name does not exist")]
    public async Task GetByNameAsync_ShouldReturnNullWhenNotFound()
    {
        // Arrange
        var nonExistentName = "NonExistent Product";

        // Act
        var result = await _repository.GetByNameAsync(nonExistentName);

        // Assert
        result.Should().BeNull();
    }

    [Theory(DisplayName = "Should throw ArgumentNullException for invalid product name")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetByNameAsync_ShouldThrowExceptionForInvalidName(string invalidName)
    {
        // Act
        Func<Task> act = async () => await _repository.GetByNameAsync(invalidName);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Product name cannot be null or empty. (Parameter 'name')");
    }
}