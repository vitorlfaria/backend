using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.DeleteSale;

/// <summary>
/// Contains unit tests for the DeleteSaleResponse class.
/// Tests cover property initialization.
/// </summary>
public class DeleteSaleResponseTests
{
    /// <summary>
    /// Tests that the Success property is correctly initialized.
    /// </summary>
    [Fact(DisplayName = "Success property should be initialized correctly")]
    public void Given_DeleteSaleResponse_When_SuccessIsSet_Then_SuccessShouldBeCorrect()
    {
        // Arrange
        var success = true;

        // Act
        var response = new DeleteSaleResponse { Success = success };

        // Assert
        Assert.Equal(success, response.Success);
    }
}