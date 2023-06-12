using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace User.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = $"Ha ocurrido un error!:\n {ex.Message}";
                _logger.LogError(message);
                await HandleExceptionAsync(context, ex);
                
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var pd = new ProblemDetails
            {
                Title = "Ha ocurrido un error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message
            };
            pd.Extensions.Add("RequestId", context.TraceIdentifier);

            return context.Response.WriteAsJsonAsync(pd, pd.GetType());
        }
    }
}
