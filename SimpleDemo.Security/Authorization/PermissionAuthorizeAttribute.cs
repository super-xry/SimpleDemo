using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace SimpleDemo.Security.Authorization
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _permissions;

        /// <summary>
        /// Creates a new instance of <see cref="AuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var policyProvider = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationPolicyProvider>();
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            var policies = ((AuthorizationPolicyProvider)policyProvider).GetPolicies(_permissions);

            var result = await authService.AuthorizeAsync(context.HttpContext.User, null, policies);

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}