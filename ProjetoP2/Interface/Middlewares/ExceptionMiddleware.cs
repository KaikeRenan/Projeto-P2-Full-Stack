using ProjetoP2.Shared.Exceptions;
using System.Net;

namespace ProjetoP2.Interface.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (status, message) = ex switch
            {
                ExceptionDuplicateCpf => (HttpStatusCode.Conflict, ex.Message),
                ExceptionDuplicateEmail => (HttpStatusCode.Conflict, ex.Message),
                ExceptionDuplicateCrmv => (HttpStatusCode.Conflict, ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
                InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
                NotImplementedException => (HttpStatusCode.NotImplemented, "Recurso não implementado"),
                _ => (HttpStatusCode.InternalServerError, ex.Message)
            };

            if (status == HttpStatusCode.InternalServerError)
                _logger.LogError(ex, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);
            else
                _logger.LogWarning(ex, "Handled exception on {Method} {Path}", context.Request.Method, context.Request.Path);

            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsJsonAsync(new { error = message });
        }

    }
}
