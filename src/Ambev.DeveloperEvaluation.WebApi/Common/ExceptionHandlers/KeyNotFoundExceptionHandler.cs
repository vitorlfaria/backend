using System;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ExceptionHandlers;

public static class KeyNotFoundExceptionHandler
{
    public static async Task HandleExceptionAsync(HttpContext context, KeyNotFoundException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var response = new ApiResponse
        {
            Success = false,
            Message = exception.Message,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
