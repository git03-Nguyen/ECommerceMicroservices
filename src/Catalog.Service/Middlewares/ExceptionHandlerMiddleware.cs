using System.Net;
using System.Text.Json;
using Contracts.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.Service.Middlewares;

public class ExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = new ErrorDetails
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = exception.Message
        };

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            error.Message = "One or more validation failures have occurred.";
        }
        else if (exception is ForbiddenAccessException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            _logger.LogWarning(exception, "Forbidden access.");
        }
        else if (exception is ResourceAlreadyExistsException resourceAlreadyExistsException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            _logger.LogWarning(exception, "Resource already exists.");
        }
        else if (exception is ResourceNotFoundException resourceNotFoundException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            _logger.LogWarning(exception, "Resource not found.");
        }
        else if (exception is UnauthorizedAccessException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _logger.LogWarning(exception, "Unauthorized access.");
        }
        else if (exception is UnAuthorizedAccessException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _logger.LogWarning(exception, "UnAuthorized access.");
        }
        else if (exception is BadHttpRequestException badHttpRequestException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _logger.LogWarning(exception, "Bad request.");
        }
        else if (exception is AuthenticationFailureException authenticationFailureException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _logger.LogWarning(exception, "Authentication failure.");
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            error.Message = "An error occurred while processing your request.: " + exception.Message;
            _logger.LogError(exception, "Unhandled exception occurred.");
        }
        
        error.StatusCode = httpContext.Response.StatusCode;

        var result = JsonSerializer.Serialize(error, _jsonSerializerOptions);
        await httpContext.Response.WriteAsync(result, cancellationToken);

        return true;
    }
}