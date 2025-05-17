using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class DeleteSaleRequestTests
{
    /// <summary>
    /// Tests that the DeleteSaleRequest is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "DeleteSaleRequest should be created with the correct Id")]
    public void Given_SaleId_When_RequestCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var saleId = Guid.NewGuid();

        // Act
        var request = new DeleteSaleRequest { Id = saleId };

        // Assert
        Assert.Equal(saleId, request.Id);
    }
}