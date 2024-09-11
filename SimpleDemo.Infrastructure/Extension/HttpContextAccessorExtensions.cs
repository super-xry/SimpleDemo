using Microsoft.AspNetCore.Http;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;
using System.Security.Claims;

namespace SimpleDemo.Infrastructure.Extension
{
    public static class HttpContextAccessorExtensions
    {
        public static string? GetCurrentUserName(this IHttpContextAccessor httpContextAccessor, bool require = true)
        {
            var currentUserName = httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (require && string.IsNullOrEmpty(currentUserName))
            {
                throw new SimpleUnauthorizedException(SimpleExceptionCode.Security.Unauthorized, "Please login first and try again.");
            }

            return currentUserName;
        }

        public static string? GetCurrentUserRole(this IHttpContextAccessor httpContextAccessor, bool require = true)
        {
            var currentUserRole = httpContextAccessor.HttpContext?.User.GetRole();

            return currentUserRole;
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user
                .Claims
                .First(c =>
                    c.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase) ||
                    c.Type.Equals(CustomClaimType.RoleId, StringComparison.OrdinalIgnoreCase))
                .Value;
        }
    }
}