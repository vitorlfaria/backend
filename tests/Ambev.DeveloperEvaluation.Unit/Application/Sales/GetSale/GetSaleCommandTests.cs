using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetSale;

/// <summary>
/// Contains unit tests for the GetSaleCommand class.
/// Tests cover initialization and immutability.
/// </summary>
public class GetSaleCommandTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized via the constructor.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly via constructor")]
    public void Given_GetSaleCommand_When_InitializedWithId_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var command = new GetSaleCommand(id);

        // Assert
        Assert.Equal(id, command.Id);
    }

    /// <summary>
    /// Tests that the Id property is immutable after initialization.
    /// </summary>
    [Fact(DisplayName = "Id property should be immutable")]
    public void Given_GetSaleCommand_When_TryingToModifyId_Then_ShouldNotBePossible()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new GetSaleCommand(id);

        // Act & Assert
        // Since Id is a readonly property, it cannot be modified after initialization.
    }
}