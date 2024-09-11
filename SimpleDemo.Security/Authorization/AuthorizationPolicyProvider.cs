using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace SimpleDemo.Security.Authorization
{
    public class AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
    {
        public AuthorizationPolicy GetPolicies(string[] policyNames)
        {
            var builder = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionAuthorizationRequirement(policyNames));

            return builder.Build();
        }
    }
}