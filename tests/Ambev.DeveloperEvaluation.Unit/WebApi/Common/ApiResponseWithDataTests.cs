using Ambev.DeveloperEvaluation.WebApi.Common;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Common;

/// <summary>
/// Contains unit tests for the ApiResponseWithData class.
/// Tests cover response creation and property assignment, including the Data property.
/// </summary>
public class ApiResponseWithDataTests
{
    /// <summary>
    /// Tests that the ApiResponseWithData is created with the correct properties, including Data.
    /// </summary>
    [Fact(DisplayName = "ApiResponseWithData should be created with the correct properties")]
    public void Given_Properties_When_ResponseCreated_Then_ShouldHaveCorrectProperties()
    {
        // Arrange
        var success = true;
        var message = "Test message";
        var data = new { Name = "Test Data", Value = 123 };

        // Act
        var response = new ApiResponseWithData<object>
        {
            Success = success,
            Message = message,
            Data = data
        };

        // Assert
        Assert.Equal(success, response.Success);
        Assert.Equal(message, response.Message);
        Assert.Equal(data, response.Data);
    }

    /// <summary>
    /// Tests that the Data property can be null.
    /// </summary>
    [Fact(DisplayName = "ApiResponseWithData should allow null Data")]
    public void Given_NullData_When_ResponseCreated_Then_DataShouldBeNull()
    {
        // Arrange & Act
        var response = new ApiResponseWithData<object>
        {
            Data = null
        };

        // Assert
        Assert.Null(response.Data);
    }
}