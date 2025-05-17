using System.Text.Json;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ExceptionHandlers;

public static class ExceptionHandler
{
    public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ApiResponse
        {
            Success = false,
            Message = "An unexpected error occurred.",
            Errors = [ new ValidationErrorDetail
                {
                    Error = "InternalServerError",
                    Detail = exception.Message
                }
            ]
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
