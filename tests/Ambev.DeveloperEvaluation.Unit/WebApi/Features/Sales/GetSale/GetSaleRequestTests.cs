using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales.GetSale;

/// <summary>
/// Contains unit tests for the GetSaleRequest class.
/// Tests cover request creation and property assignment.
/// </summary>
public class GetSaleRequestTests
{
    /// <summary>
    /// Tests that the GetSaleRequest is created with the correct Id.
    /// </summary>
    [Fact(DisplayName = "GetSaleRequest should be created with the correct Id")]
    public void Given_SaleId_When_RequestCreated_Then_ShouldHaveCorrectId()
    {
        // Arrange
        var saleId = Guid.NewGuid();

        // Act
        var request = new GetSaleRequest { Id = saleId };

        // Assert
        Assert.Equal(saleId, request.Id);
    }
}