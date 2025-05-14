using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Middleware;

/// <summary>
/// Contains unit tests for the ValidationExceptionMiddleware class.
/// Tests cover handling of ValidationException and formatting of the response.
/// </summary>
public class ValidationExceptionMiddlewareTests
{
    /// <summary>
    /// Tests that the middleware correctly handles a ValidationException and returns a 400 Bad Request response.
    /// </summary>
    [Fact(DisplayName = "Should handle ValidationException and return 400 Bad Request")]
    public async Task InvokeAsync_WithValidationException_ShouldReturnBadRequest()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("Property1", "Error message 1"),
            new ValidationFailure("Property2", "Error message 2")
        };
        var validationException = new ValidationException(validationErrors);

        RequestDelegate next = _ => throw validationException;
        var middleware = new ValidationExceptionMiddleware(next);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        context.Response.ContentType.Should().Be("application/json");

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseBody, options);

        apiResponse.Should().NotBeNull();
        apiResponse!.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("Validation Failed");
        apiResponse.Errors.Should().BeEquivalentTo(validationErrors.Select(e => new ValidationErrorDetail { Error = e.ErrorCode, Detail = e.ErrorMessage }));
    }

    /// <summary>
    /// Tests that the middleware calls the next delegate when no exception is thrown.
    /// </summary>
    [Fact(DisplayName = "Should call next delegate when no exception is thrown")]
    public async Task InvokeAsync_WithoutException_ShouldCallNextDelegate()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextDelegateCalled = false;
        RequestDelegate next = _ =>
        {
            nextDelegateCalled = true;
            return Task.CompletedTask;
        };
        var middleware = new ValidationExceptionMiddleware(next);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        nextDelegateCalled.Should().BeTrue();
        context.Response.StatusCode.Should().Be(StatusCodes.Status200OK); // Default status code
    }
}