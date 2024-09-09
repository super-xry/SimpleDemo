namespace SimpleDemo.Domain.DbEntity.CommerceEntity
{
    public interface ICommerceRepository : ISimpleDemoRepository
    {
        IQueryable<UserEntity> Users { get; }
    }
}
