namespace SimpleDemo.Domain.DbEntity.Common
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}