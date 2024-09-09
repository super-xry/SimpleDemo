using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity.Common;

namespace SimpleDemo.Infrastructure.Repository
{
    public class DbContextUnitOfWork(DbContext context) : IUnitOfWork
    {
        public DbContext DbContext { get; } = context;

        public int Commit()
        {
            var result = DbContext.SaveChanges();
            return result;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}