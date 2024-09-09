using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity;
using SimpleDemo.Domain.DbEntity.Common;

namespace SimpleDemo.Infrastructure.Repository
{
    public abstract class BaseSimpleDemoRepository<TContext>(TContext dbContext, IUnitOfWork? unitOfWork = null)
        : ISimpleDemoRepository
        where TContext : DbContext
    {
        public void Dispose()
        {
            DbContext.Dispose();
            UnitOfWork = null!;
            DbContext = null!;
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<RoleEntity> Roles => DbContext.Set<RoleEntity>();

        public virtual IQueryable<PermissionEntity> Permissions => DbContext.Set<PermissionEntity>();

        public virtual IQueryable<LogEntity> LogEntities => DbContext.Set<LogEntity>();

        public TContext DbContext { get; private set; } = dbContext;

        public IUnitOfWork UnitOfWork { get; private set; } = unitOfWork ?? new DbContextUnitOfWork(dbContext);

        public virtual void Attach<T>(T item, EntityState state = EntityState.Modified) where T : class
        {
            var entry = DbContext.Attach(item);
            entry.State = state;
        }

        public virtual void Add<T>(T item) where T : class
        {
            DbContext.Add(item);
        }

        public virtual void AddRange<T>(ICollection<T> items) where T : class
        {
            DbContext.AddRange(items);
        }

        public virtual void Update<T>(T item) where T : class
        {
            DbContext.Update(item);
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public virtual void Remove<T>(T item) where T : class
        {
            DbContext.Remove(item);
        }

        public virtual void RemoveRange<T>(ICollection<T> items) where T : class
        {
            DbContext.RemoveRange(items);
        }

        public void RemoveRange<T>(IQueryable<T> items) where T : class
        {
            DbContext.RemoveRange(items);
        }
    }
}
