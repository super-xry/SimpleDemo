using SimpleDemo.Infrastructure.Messages;

namespace SimpleDemo.Infrastructure.Events
{
    public interface IEventHandler<in T> : IHandler<T> where T : IEvent
    {
    }
}