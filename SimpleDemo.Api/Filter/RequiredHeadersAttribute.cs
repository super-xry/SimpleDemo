using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleDemo.Api.Filter
{
    public class RequiredHeadersAttribute(params string[] requiredHeaderNames) : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var requiredHeaderName in requiredHeaderNames)
            {
                var requiredHeaderValue = context.HttpContext.Request.Headers[requiredHeaderName].FirstOrDefault();
                if (string.IsNullOrEmpty(requiredHeaderValue))
                {
                    context.ModelState.AddModelError(requiredHeaderName, $"Header '{requiredHeaderName}' is required.");
                }
            }
        }
    }
}