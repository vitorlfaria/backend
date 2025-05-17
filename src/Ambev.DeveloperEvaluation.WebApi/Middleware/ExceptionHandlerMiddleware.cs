using Ambev.DeveloperEvaluation.WebApi.Common.ExceptionHandlers;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            await InvalidOperationExceptionHandler.HandleExceptionAsync(context, ex);
        }
        catch (KeyNotFoundException ex)
        {
            await KeyNotFoundExceptionHandler.HandleExceptionAsync(context, ex);
        }
        catch (ValidationException ex)
        {
            await ValidationExceptionHandler.HandleValidationExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            await UnauthorizedAccessExcenptionHandler.HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await ExceptionHandler.HandleExceptionAsync(context, ex);
        }
    }

}
