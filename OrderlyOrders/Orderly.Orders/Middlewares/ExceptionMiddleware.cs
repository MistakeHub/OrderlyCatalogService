using Microsoft.EntityFrameworkCore;

namespace Orderly.Orders.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    Errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error");

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(_env.IsDevelopment() ? $"{ex.Message}, {ex.StackTrace}" : "Database Unexpected error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(_env.IsDevelopment() ? $"{ex.Message}, {ex.StackTrace}" : "Unexpected server error");
            }
        }
    }
}
