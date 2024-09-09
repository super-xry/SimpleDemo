using SimpleDemo.Domain.DbEntity.AdminEntity;
using SimpleDemo.Domain.DbEntity.Common;

namespace SimpleDemo.Infrastructure.Repository
{
    public class AdminRepository(AdminDbContext dbContext, IUnitOfWork? unitOfWork = null)
        : BaseSimpleDemoRepository<AdminDbContext>(dbContext, unitOfWork), IAdminRepository
    {
        public IQueryable<UserEntity> Users => DbContext.Set<UserEntity>();
    }
}
