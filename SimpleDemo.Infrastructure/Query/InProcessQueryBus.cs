using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;

namespace SimpleDemo.Infrastructure.Query
{
    public class InProcessQueryBus(Func<Type, IQueryHandler> getQueryHandler) : IQueryBus
    {
        public virtual async Task<TQueryResult> SendAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery
        {
            var queryHandler = (IQueryHandler<TQuery, Task<TQueryResult>>)getQueryHandler(typeof(IQueryHandler<TQuery, Task<TQueryResult>>));

            if (queryHandler == null)
            {
                throw new SimpleDemoNotFoundException(SimpleExceptionCode.Application.QueryHandlerNotFound, "Query handler not found");
            }

            return await queryHandler.HandleAsync(query, cancellationToken);
        }
    }
}