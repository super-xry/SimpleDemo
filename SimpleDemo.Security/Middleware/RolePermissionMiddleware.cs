using Microsoft.AspNetCore.Http;
using SimpleDemo.Security.Services;
using SimpleDemo.Shared.Constant;
using System.Security.Claims;

namespace SimpleDemo.Security.Middleware
{
    public class RolePermissionMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext, IPermissionService permissionService)
        {
            if (httpContext.User.Identity is { IsAuthenticated: true })
            {
                var roleIdString = httpContext.User.Claims
                    .First(c => c.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))
                    .Value;
                var roleId = Guid.Parse(roleIdString);
                var permissions = await permissionService.GetPermissionsAsync(roleId, default);
                var claims = permissions.Select(permission => new Claim(CustomClaimType.Permission, permission));
                var appIdentity = new ClaimsIdentity(claims);
                httpContext.User.AddIdentity(appIdentity);
            }

            await next(httpContext);
        }
    }
}