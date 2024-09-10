using SimpleDemo.Domain.DbEntity.Common;
using SimpleDemo.Infrastructure.Messages;

namespace SimpleDemo.Infrastructure.Events
{
    public interface IEvent : IEntity, IMessage
    {
        DateTimeOffset TimeStamp { get; set; }
    }
}