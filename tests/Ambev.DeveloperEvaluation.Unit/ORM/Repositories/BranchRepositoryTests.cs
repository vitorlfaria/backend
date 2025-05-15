using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories;

public class BranchRepositoryTests : IClassFixture<DbContextFixture>
{
    private readonly BranchRespository _repository;
    private readonly DefaultContext _context;

    public BranchRepositoryTests(DbContextFixture fixture)
    {
        _context = fixture.Context;
        _repository = new BranchRespository(_context);
    }

    [Fact(DisplayName = "Should get a branch by name successfully")]
    public async Task GetByNameAsync_ShouldReturnBranch()
    {
        // Arrange
        var branchName = "Test Branch";
        var branch = new Branch { Name = branchName };
        await _repository.CreateAsync(branch);

        // Act
        var result = await _repository.GetByNameAsync(branchName);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Contain(branchName);
    }

    [Fact(DisplayName = "Should return null when branch name does not exist")]
    public async Task GetByNameAsync_ShouldReturnNullWhenNotFound()
    {
        // Arrange
        var nonExistentName = "NonExistent Branch";

        // Act
        var result = await _repository.GetByNameAsync(nonExistentName);

        // Assert
        result.Should().BeNull();
    }

    [Theory(DisplayName = "Should throw ArgumentNullException for invalid branch name")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetByNameAsync_ShouldThrowExceptionForInvalidName(string invalidName)
    {
        // Act
        Func<Task> act = async () => await _repository.GetByNameAsync(invalidName);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Branch name cannot be null or empty. (Parameter 'name')");
    }
}