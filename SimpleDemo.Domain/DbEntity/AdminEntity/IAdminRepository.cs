namespace SimpleDemo.Domain.DbEntity.AdminEntity
{
    public interface IAdminRepository : ISimpleDemoRepository
    {
        IQueryable<UserEntity> Users { get; }
    }
}
