using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimpleDemo.Infrastructure.Common;
using SimpleDemo.Infrastructure.Utility;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;
using System.Diagnostics;

namespace SimpleDemo.Application
{
    public static class CustomErrorHandler
    {
        private const string SystemError = "A system error has occurred.";

        public static void UseCustomErrors(this IApplicationBuilder app, bool isDevelopment)
        {
            if (isDevelopment)
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => OnExceptionAsync(httpContext, includeDetail: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => OnExceptionAsync(httpContext, includeDetail: false);

        private static async Task OnExceptionAsync(HttpContext httpContext, bool includeDetail)
        {
            var handler = httpContext.Features.Get<IExceptionHandlerFeature>();
            var originalException = handler?.Error;
            if (originalException == null) return;
            var exception = ConvertException(originalException);

            // Todo: write exception into db, if we don't care the saving result the just remove the
            // keyword 'await' here
            var logService = httpContext.RequestServices.GetRequiredService<ILogService>();
            await logService.LogErrorAsync(SystemError, exception);

            var finialDetails = HandleException(exception, httpContext, includeDetail);
            httpContext.Response.StatusCode = (int)exception.StatusCode;
            await WriteResponseAsync(httpContext, finialDetails);
        }

        private static SimpleBaseException ConvertException(Exception exception)
        {
            if (exception is SimpleBaseException simpleBaseException) return simpleBaseException;

            // Todo: handle the error message with difference type
            /*if (exception.GetType() == typeof(RpcException) || exception.InnerException?.GetType() == typeof(RpcException))
            {
                message = ((dynamic)(exception.InnerException ?? exception)).Status?.Detail ?? SystemError;
            }*/

            return new SimpleUnknownException(SimpleExceptionCode.Application.UnknownException, exception.Message);
        }

        private static ProblemDetails HandleException(SimpleBaseException exception, HttpContext httpContext, bool includeDetail)
        {
            var statusCode = (int)exception.StatusCode;
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = exception is SimpleUnknownException ? SystemError : exception.ErrorCode,
                Detail = includeDetail ? exception.ToString() : null,
                Extensions = { ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier } // FYI: just an example, can add more detail info into the extensions.
            };
            return problemDetails;
        }

        private static async Task WriteResponseAsync(HttpContext httpContext, ProblemDetails problemDetails)
        {
            httpContext.Response.ContentType = "application/problem+json";
            var json = JsonUtility.SerializeObject(problemDetails)!;
            await httpContext.Response.WriteAsync(json);
        }
    }
}