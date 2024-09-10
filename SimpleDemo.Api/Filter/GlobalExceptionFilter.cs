using Microsoft.AspNetCore.Mvc.Filters;
using SimpleDemo.Infrastructure.Common;

namespace SimpleDemo.Api.Filter
{
    public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, ILogService logService)
        : ActionFilterAttribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // Todo: do something here.
            logger.LogError("Meet an error: {message}", context.Exception.Message);

            await logService.LogErrorAsync("", context.Exception);
        }
    }
}