namespace SimpleDemo.Infrastructure.Messages
{
    public interface IHandler<in T> where T : IMessage
    {
        Task HandleAsync(T message, CancellationToken cancellationToken);
    }
}