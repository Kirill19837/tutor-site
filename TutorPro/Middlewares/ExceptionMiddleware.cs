
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var jsonResponse = JsonConvert.SerializeObject(response);

            await httpContext.Response.WriteAsync(jsonResponse);
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                DirectoryNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception)
        {
            return exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };
        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = (IReadOnlyDictionary<string, string[]>?)validationException?.Data;
            }
            return errors;
        }
    }
}
