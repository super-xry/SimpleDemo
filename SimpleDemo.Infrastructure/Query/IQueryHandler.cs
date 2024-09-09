namespace SimpleDemo.Infrastructure.Query
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery
    {
        TResult HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}