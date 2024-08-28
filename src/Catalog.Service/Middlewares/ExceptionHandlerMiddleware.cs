using System.Net;
using System.Text.Json;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.Service.Middlewares;
 
public class ExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

        var error = new ErrorDetails
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = "An error occurred while processing your request."
        };

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            error.Message = "One or more validation failures have occurred.";
        }
        else if (exception is CategoryNotFoundException categoryNotFoundException)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            error.Message = $"Category with id {categoryNotFoundException.RequestId} not found.";
        }
        else if (exception is ProductNotFoundException productNotFoundException)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            error.Message = $"Product with id {productNotFoundException.RequestId} not found.";
        }
        else
        {
            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            error.Message = exception.Message;
        }

        var result = JsonSerializer.Serialize(error, _jsonSerializerOptions);
        await httpContext.Response.WriteAsync(result, cancellationToken);

        return true;
        
    }
}