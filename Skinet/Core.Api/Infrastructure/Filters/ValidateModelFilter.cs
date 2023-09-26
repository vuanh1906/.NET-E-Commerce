using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Core.Api.Infrastructure.Filters
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelFilter> _logger;
        public ValidateModelFilter(ILogger<ValidateModelFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogError("Model state is not valid.");

                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
