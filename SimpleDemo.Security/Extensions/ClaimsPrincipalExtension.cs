using SimpleDemo.Shared.Constant;
using System.Security.Claims;

namespace SimpleDemo.Security.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static bool HasPermission(this ClaimsPrincipal principal, string permission)
        {
            var resource = permission.Split(".").First();
            var allPermissions = FindPermissions(principal);
            return allPermissions.Any(p => p.Equals($"{resource}.FullAccess", StringComparison.OrdinalIgnoreCase)) || allPermissions.Any(p => p.ToLower().Equals(permission.ToLower(), StringComparison.OrdinalIgnoreCase));
        }

        private static IReadOnlyList<string> FindPermissions(ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));

            var permissions = principal.Claims
                .Where(c => c.Type.Equals(CustomClaimType.Permission, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Value)
                .ToList();

            Console.WriteLine($"Permissions: {string.Join(",", permissions)}");

            return permissions.AsReadOnly();
        }
    }
}