using SimpleDemo.Domain.DbEntity.Common;

namespace SimpleDemo.Domain.DbEntity
{
    public interface ISimpleDemoRepository : IDisposable
    {
        IQueryable<RoleEntity> Roles { get; }

        IQueryable<PermissionEntity> Permissions { get; }

        IQueryable<LogEntity> LogEntities { get; }

        IUnitOfWork UnitOfWork { get; }
        
        void Add<T>(T item) where T : class;

        void AddRange<T>(ICollection<T> item) where T : class;

        void Update<T>(T item) where T : class;

        void Remove<T>(T item) where T : class;

        void RemoveRange<T>(ICollection<T> items) where T : class;
    }
}
