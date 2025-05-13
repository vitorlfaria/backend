using Ambev.DeveloperEvaluation.Domain.Common;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Common;

/// <summary>
/// Contains unit tests for the BaseEntity class.
/// Tests cover Id comparison scenarios.
/// </summary>
public class BaseEntityTests
{
    /// <summary>
    /// Tests that CompareTo returns 0 when comparing an entity to itself.
    /// </summary>
    [Fact(DisplayName = "CompareTo should return 0 when comparing an entity to itself")]
    public void Given_SameEntity_When_Compared_Then_ShouldReturnZero()
    {
        // Arrange
        var entity = new BaseEntity { Id = Guid.NewGuid() };

        // Act
        var result = entity.CompareTo(entity);

        // Assert
        Assert.Equal(0, result);
    }

    /// <summary>
    /// Tests that CompareTo returns 1 when comparing an entity to null.
    /// </summary>
    [Fact(DisplayName = "CompareTo should return 1 when comparing an entity to null")]
    public void Given_EntityAndNull_When_Compared_Then_ShouldReturnOne()
    {
        // Arrange
        var entity = new BaseEntity { Id = Guid.NewGuid() };

        // Act
        var result = entity.CompareTo(null);

        // Assert
        Assert.Equal(1, result);
    }
}
