using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleCommand class.
/// Tests cover initialization and immutability.
/// </summary>
public class DeleteSaleCommandTests
{
    /// <summary>
    /// Tests that the Id property is correctly initialized via the constructor.
    /// </summary>
    [Fact(DisplayName = "Id property should be initialized correctly via constructor")]
    public void Given_DeleteSaleCommand_When_InitializedWithId_Then_IdShouldBeCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var command = new DeleteSaleCommand(id);

        // Assert
        Assert.Equal(id, command.Id);
    }
}