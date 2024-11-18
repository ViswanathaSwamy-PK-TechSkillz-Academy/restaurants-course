using Microsoft.EntityFrameworkCore.Storage;
using Restaurants.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    // Cache and reuse JsonSerializerOptions instance
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            logger.LogWarning(notFound, "Exception Type: {ExceptionType}, Message: {Message}", notFound.GetType().Name, notFound.Message);
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, notFound.Message);
        }
        catch (InvalidOperationException invalidOperation)
        {
            logger.LogWarning(invalidOperation, "Exception Type: {ExceptionType}, Message: {Message}", invalidOperation.GetType().Name, invalidOperation.Message);
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, invalidOperation.Message);
        }
        catch (RetryLimitExceededException retryLimitExceededException)
        {
            logger.LogWarning(retryLimitExceededException, "Exception Type: {ExceptionType}, Message: {Message}", retryLimitExceededException.GetType().Name, retryLimitExceededException.Message);
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Retry limit exceeded");
        }
        catch (ForbidException forbidException)
        {
            context.Response.StatusCode = 403;
            logger.LogWarning(forbidException, "Exception Type: {ExceptionType}, Message: {Message}", forbidException.GetType().Name, forbidException.Message);
            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception Type: {ExceptionType}, Message: {Message}", ex.GetType().Name, ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message
        };

        var jsonResponse = JsonSerializer.Serialize(response, _jsonSerializerOptions);

        return context.Response.WriteAsync(jsonResponse);
    }
}