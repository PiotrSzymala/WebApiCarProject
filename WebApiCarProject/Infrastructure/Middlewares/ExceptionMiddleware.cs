using System.Net;
using Microsoft.OpenApi.Extensions;
using WebApiCar.Infrastructure.Errors;
using WebApiCar.Infrastructure.Exceptions;
using ApplicationException = WebApiCar.Infrastructure.Exceptions.ApplicationException;

namespace WebApiCar.Infrastructure.Middlewares
{
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

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    throw new Exception();
                }
            }
            catch (ApplicationException ex)
            {
                _logger.LogError($"{ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception unrecognizedException)
            {
                _logger.LogError($"{unrecognizedException}");
                await HandleExceptionAsync(httpContext, unrecognizedException);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ApplicationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            string title = exception.GetType().Name;
            string message = GetMessage(exception);

            await context.Response.WriteAsync(
                new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Title = title,
                    Message = message
                }.ToString()
            );
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string title = HttpStatusCode.InternalServerError.GetDisplayName();
            string message = exception.Message;

            await context.Response.WriteAsync(
                new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Title = title,
                    Message = message
                }.ToString()
            );
        }

        private string GetMessage(ApplicationException exception)
        {
            if (exception is ValidationException validationException)
            {
                List<string> errorMessages = new();

                foreach (var error in validationException.Errors)
                {
                    errorMessages.Add($"{error.Key}:[{string.Join(", ", error.Value)}]");
                }

                return string.Join(", ", errorMessages);
            }

            return exception.Message;
        }
    }
}
