namespace SimpleDemo.Infrastructure.Query
{
    public interface IQueryBus
    {
        Task<TQueryResult> SendAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery;
    }
}