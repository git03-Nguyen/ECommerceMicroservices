using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using User.Service.Exceptions;

namespace User.Service.Middlewares;

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
            Message = "An error occurred while processing your request."
        };

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            error.Message = "One or more validation failures have occurred.";
        }
        else
        {
            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            error.Message = exception.Message;
        }

        var result = JsonSerializer.Serialize(error, _jsonSerializerOptions);
        await httpContext.Response.WriteAsync(result, cancellationToken);

        return true;
    }
}