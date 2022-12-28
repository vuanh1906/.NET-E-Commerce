using Microsoft.AspNetCore.Mvc;
using Skinet.Errors;
using System.Net;
using System.Text.Json;

namespace Skinet.Middleware
{
    public class ExceptionMiddleware 
    {
        private RequestDelegate _next { get; set; }
        private ILogger<ExceptionMiddleware> _logger { get; set; }
        private IHostEnvironment _env { get; set; }
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                        ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }

    }
}
