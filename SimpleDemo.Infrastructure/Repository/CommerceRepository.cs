using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Domain.DbEntity.Common;

namespace SimpleDemo.Infrastructure.Repository
{
    public class CommerceRepository(CommerceDbContext dbContext, IUnitOfWork? unitOfWork = null)
        : BaseSimpleDemoRepository<CommerceDbContext>(dbContext, unitOfWork), ICommerceRepository
    {
        public IQueryable<UserEntity> Users => DbContext.Set<UserEntity>();
    }
}