using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ManagementEmployee.Repository.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                // Check for unauthorized or forbidden responses
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await HandleExceptionAsync(context, new UnauthorizedAccessException("You are not authorized to access this resource."));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await HandleExceptionAsync(context, new UnauthorizedAccessException("You do not have permission to access this resource."));
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = context.Response.StatusCode != null ? context.Response.StatusCode : (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message!=null ? exception.Message : "You are not authorized to access this resource."
                }.ToString());
            }
            if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = context.Response.StatusCode != null ? context.Response.StatusCode : (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message!=null ? exception.Message : "You are not authorized to access this resource."
                }.ToString());
            }
            else if (exception is InvalidOperationException)
            {
                context.Response.StatusCode = context.Response.StatusCode != null ? context.Response.StatusCode : (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message != null ? exception.Message : "Invalid operation."
                }.ToString());
            }

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message != null ? exception.Message : "Internal Server Error from the custom middleware."
            }.ToString());
        }
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}
