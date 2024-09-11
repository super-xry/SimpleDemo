using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Infrastructure.Extension;
using SimpleDemo.Security.Services;
using System.Data;

namespace SimpleDemo.Application.Authorization
{
    // Todo: using cache here...
    public class PermissionService(Func<ICommerceRepository> repositoryFactory, IHttpContextAccessor httpContextAccessor) : IPermissionService
    {
        public async Task<List<string>> GetPermissionsAsync(Guid roleId, CancellationToken cancellationToken)
        {
            if (roleId == Guid.Empty) return [];

            using var repository = repositoryFactory();

            var permissions = await repository
                .Permissions.Where(it => it.RoleId == roleId)
                .Select(it => it.Label)
                .ToListAsync(cancellationToken);

            return permissions;
        }

        public Task UpdateAsync(Guid roleId, List<string> permissions, CancellationToken cancellationToken)
        {
            var currentUserName = httpContextAccessor.GetCurrentUserName();
            using var repository = repositoryFactory();

            // Todo: update permission logic here...

            throw new NotImplementedException();
        }
    }
}