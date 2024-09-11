using Microsoft.AspNetCore.Authorization;
using SimpleDemo.Security.Extensions;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;

namespace SimpleDemo.Security.Authorization
{
    public sealed class PermissionAuthorizationRequirement(IEnumerable<string> permissions)
        : AuthorizationHandler<PermissionAuthorizationRequirement>, IAuthorizationRequirement
    {
        public IEnumerable<string> Permissions { get; } = permissions ?? throw new ArgumentNullException(nameof(permissions));

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (!requirement.Permissions.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.User.Identity is { IsAuthenticated: false })
            {
                throw new SimpleUnauthorizedException(SimpleExceptionCode.Security.Unauthorized, "User info is outdated, please login again");
            }

            // Todo: Full access permission => context.User.HasPermission(??)
            var hasPermission = requirement.Permissions.Any(context.User.HasPermission);

            if (!hasPermission)
            {
                throw new SimpleForbiddenException(SimpleExceptionCode.Security.AccessDenied, "Access denied.");
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}