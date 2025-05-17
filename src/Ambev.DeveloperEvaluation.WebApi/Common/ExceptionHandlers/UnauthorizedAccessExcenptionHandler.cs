using System;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ExceptionHandlers;

public static class UnauthorizedAccessExcenptionHandler
{
    public static async Task HandleExceptionAsync(HttpContext context, UnauthorizedAccessException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var response = new ApiResponse
        {
            Success = false,
            Message = exception.Message
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}
