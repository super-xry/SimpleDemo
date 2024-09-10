using SimpleDemo.Infrastructure.Events;

namespace SimpleDemo.Application.Events
{
    public class LoginEvent : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string From { get; set; } = null!;

        public Guid? UserId { get; set; }

        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
    }
}