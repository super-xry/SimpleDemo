namespace SimpleDemo.Security.Services
{
    public interface IPermissionService
    {
        Task<List<string>> GetPermissionsAsync(Guid roleId, CancellationToken cancellationToken);

        Task UpdateAsync(Guid roleId, List<string> permissions, CancellationToken cancellationToken);
    }
}