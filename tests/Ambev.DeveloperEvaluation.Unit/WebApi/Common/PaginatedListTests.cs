using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Unit.TestExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Common;

/// <summary>
/// Contains unit tests for the PaginatedList class.
/// Tests cover list creation, pagination properties, and helper methods.
/// </summary>
public class PaginatedListTests
{
    /// <summary>
    /// Tests that the PaginatedList is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "PaginatedList should be created with the correct properties")]
    public void Given_ItemsAndPaginationInfo_When_ListCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 };
        var count = 10;
        var pageNumber = 2;
        var pageSize = 3;

        // Act
        var paginatedList = new PaginatedList<int>(items, count, pageNumber, pageSize);

        // Assert
        Assert.Equal(items, paginatedList);
        Assert.Equal(count, paginatedList.TotalCount);
        Assert.Equal(pageNumber, paginatedList.CurrentPage);
        Assert.Equal(pageSize, paginatedList.PageSize);
        Assert.Equal(4, paginatedList.TotalPages); // Calculated as ceiling(10 / 3)
    }

    /// <summary>
    /// Tests that HasPrevious is true when the current page is greater than 1.
    /// </summary>
    [Fact(DisplayName = "HasPrevious should be true when not on the first page")]
    public void Given_NotFirstPage_When_CheckingHasPrevious_Then_ShouldReturnTrue()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>(new List<int>(), 10, 2, 3);

        // Act & Assert
        Assert.True(paginatedList.HasPrevious);
    }

    /// <summary>
    /// Tests that HasPrevious is false when the current page is 1.
    /// </summary>
    [Fact(DisplayName = "HasPrevious should be false when on the first page")]
    public void Given_FirstPage_When_CheckingHasPrevious_Then_ShouldReturnFalse()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>(new List<int>(), 10, 1, 3);

        // Act & Assert
        Assert.False(paginatedList.HasPrevious);
    }

    /// <summary>
    /// Tests that HasNext is true when the current page is less than the total pages.
    /// </summary>
    [Fact(DisplayName = "HasNext should be true when not on the last page")]
    public void Given_NotLastPage_When_CheckingHasNext_Then_ShouldReturnTrue()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>(new List<int>(), 10, 2, 3);

        // Act & Assert
        Assert.True(paginatedList.HasNext);
    }

    /// <summary>
    /// Tests that HasNext is false when the current page is equal to the total pages.
    /// </summary>
    [Fact(DisplayName = "HasNext should be false when on the last page")]
    public void Given_LastPage_When_CheckingHasNext_Then_ShouldReturnFalse()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>(new List<int>(), 10, 4, 3);

        // Act & Assert
        Assert.False(paginatedList.HasNext);
    }

    /// <summary>
    /// Tests that CreateAsync method returns a PaginatedList with the correct properties.
    /// </summary>
    [Fact(DisplayName = "CreateAsync should return a PaginatedList with the correct properties")]
    public async Task Given_QueryableAndPaginationInfo_When_CreatingPaginatedList_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 }.AsAsyncQueryable();
        var count = items.Count();
        var pageNumber = 1;
        var pageSize = 2;

        // Act
        var paginatedList = await PaginatedList<int>.CreateAsync(items, pageNumber, pageSize);

        // Assert
        Assert.Equal(2, paginatedList.Count);
        Assert.Equal(count, paginatedList.TotalCount);
        Assert.Equal(pageNumber, paginatedList.CurrentPage);
        Assert.Equal(pageSize, paginatedList.PageSize);
    }
}