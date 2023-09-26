using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace Core.Api.Infrastructure.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        private readonly List<string> safeErrors = new()
        {
            "Notifications.Api.Features.Chat.ChatController.SendPhoto (Notifications.Api) The client has disconnected",
            "Notifications.Api.Features.Chat.ChatController.SendPhoto (Notifications.Api) Unexpected end of request content."
        };

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            string message = string.Empty;
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ApiException))
            {
                var exception = context.Exception;
                status = ((ApiException)exception).Status;

                if (((ApiException)exception).IsSafeMessage)
                {
                    _logger.LogInformation($"ApiException: {exception.Message}");
                    context.Result = new JsonResult(new { Exception = exception.Message });
                }
                else
                {
                    _logger.LogCritical($"ApiException (InternalServerError): {exception}");
                    context.Result = new JsonResult(new { Exception = HttpStatusCode.InternalServerError });
                }
            }
            else
            {
                if (safeErrors.Contains($"{context.ActionDescriptor.DisplayName} {context.Exception.Message}"))
                    _logger.LogWarning(context.Exception, context.ActionDescriptor.DisplayName);
                else
                {
                    _logger.LogCritical(context.Exception, context.ActionDescriptor.DisplayName);
                    _logger.LogInformation($"StackTrace: {context.Exception.StackTrace}");
                }

                context.Result = new JsonResult(new { Exception = HttpStatusCode.InternalServerError.ToString() });
            }

            context.HttpContext.Response.StatusCode = (int)status;
        }
    }
}
