using SimpleDemo.Infrastructure.Events;
using SimpleDemo.Infrastructure.Messages;

namespace SimpleDemo.Infrastructure.Bus
{
    public class InProcessBus : IEventPublisher, IHandlerRegistrar
    {
        private readonly Dictionary<Type, List<Func<IMessage, CancellationToken, Task>>> _routes = new();

        public void RegisterHandler<T>(Func<T, CancellationToken, Task> handler) where T : class, IMessage
        {
            if (!_routes.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Func<IMessage, CancellationToken, Task>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((message, token) => handler((T)message, token));
        }

        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
        {
            if (!EventSuppressor.EventsSuppressed && _routes.TryGetValue(@event.GetType(), out var handlers))
            {
                await Task.WhenAll(handlers.Select(handler => handler(@event, cancellationToken)));
            }
        }
    }
}