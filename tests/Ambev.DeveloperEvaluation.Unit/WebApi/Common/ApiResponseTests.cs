using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Common;

/// <summary>
/// Contains unit tests for the ApiResponse class.
/// Tests cover response creation and property assignment.
/// </summary>
public class ApiResponseTests
{
    /// <summary>
    /// Tests that the ApiResponse is created with the correct properties.
    /// </summary>
    [Fact(DisplayName = "ApiResponse should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var success = true;
        var message = "Test message";
        var errors = new List<ValidationErrorDetail>
        {
            new() { Error = "Error1", Detail = "Detail1" },
            new() { Error = "Error2", Detail = "Detail2" }
        };

        // Act
        var response = new ApiResponse
        {
            Success = success,
            Message = message,
            Errors = errors
        };

        // Assert
        Assert.Equal(success, response.Success);
        Assert.Equal(message, response.Message);
        Assert.Equal(errors, response.Errors);
    }
}