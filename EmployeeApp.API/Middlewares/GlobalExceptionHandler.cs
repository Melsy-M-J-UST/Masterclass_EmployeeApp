using EmployeeApp.API.Dto;
using EmployeeApp.API.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An Unexpected error occured : {Message}", exception.Message);
            var (Statuscode, Message) = exception switch{
                EmployeeNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                InvalidAgeException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, exception.Message) //General Exception Handler
            };
            var response = new ErrorResponse
            {
                StatusCode = Statuscode,
                Message = Message,
                Timestamp = DateTime.UtcNow,
                Path = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = Statuscode;
            await httpContext.Response.WriteAsJsonAsync(response);
            return true;
        }
    }
}
