using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HSZF_09_Practice.WebAPI.Exceptions
{
    public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            logger.LogError(exception, "[{Time}] Error message: {Message}", DateTime.UtcNow, exception.Message);

            ProblemDetails details = new ProblemDetails()
            {
                Title = exception.GetType().Name,
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError, // You can build a fallback logic on statuscodes to handle access denie etc.
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = details.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

            return true;
        }
    }
}
