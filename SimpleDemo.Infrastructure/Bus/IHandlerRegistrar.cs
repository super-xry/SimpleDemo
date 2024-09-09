using SimpleDemo.Infrastructure.Messages;

namespace SimpleDemo.Infrastructure.Bus
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Func<T, CancellationToken, Task> handler) where T : class, IMessage;
    }
}