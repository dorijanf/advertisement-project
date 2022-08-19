using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedModels.Exceptions;

namespace backend_template.Middleware
{
    /// <summary>
    /// The exception handler middleware handles all exception thrown by the application deliberately and
    /// also picks up on unhandled exceptions which the server couldn't properly execute. Based on the
    /// type of the thrown exception, the middleware returns an appropriate error code.
    /// </summary>
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandler> logger;

        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Based on the type of the exception, execute a switch statement which returns the
        /// appropriate error code alongside the given error message.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                BadRequestException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json; charset=UTF-8";
            var exceptionMessage = exception.Message;
            if (exception.InnerException != null)
            {
                exceptionMessage += " InnerException:" + exception.InnerException.Message;
            }
            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = exceptionMessage
            }));
        }
    }
}
