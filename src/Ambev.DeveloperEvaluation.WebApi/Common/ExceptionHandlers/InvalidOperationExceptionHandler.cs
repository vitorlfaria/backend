using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ExceptionHandlers;

public static class InvalidOperationExceptionHandler
{
    public static async Task HandleExceptionAsync(HttpContext context, InvalidOperationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ApiResponse
        {
            Success = false,
            Message = exception.Message,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
