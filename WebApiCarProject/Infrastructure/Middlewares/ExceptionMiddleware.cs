using System.Net;
using Microsoft.OpenApi.Extensions;
using WebApiCarProject.Infrastructure.Errors;
using WebApiCarProject.Infrastructure.Exceptions;
using ApplicationException = WebApiCarProject.Infrastructure.Exceptions.ApplicationException;

namespace WebApiCarProject.Infrastructure.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "NotFoundException occurred");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "ApplicationException occurred");
            await HandleExceptionAsync(httpContext, ex, (HttpStatusCode)ex.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var title = exception.GetType().Name;
        var message = exception.Message;

        if (exception is ApplicationException appException)
        {
            title = appException.GetType().Name;
            message = GetMessage(appException);
        }

        var errorDetails = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Title = title,
            Message = message
        };

        await context.Response.WriteAsync(errorDetails.ToString());
    }

    private string GetMessage(ApplicationException exception)
    {
        if (exception is ValidationException validationException)
        {
            var errorMessages = validationException.Errors
                .Select(error => $"{error.Key}: [{string.Join(", ", error.Value)}]")
                .ToList();

            return string.Join(", ", errorMessages);
        }

        return exception.Message;
    }
}
