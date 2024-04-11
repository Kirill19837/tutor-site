
using Microsoft.AspNetCore.Mvc;

namespace TutorPro.Middlewares
{
    public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                logger.LogError($"Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId : {traceId}",
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
