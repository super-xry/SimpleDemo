namespace SimpleDemo.Infrastructure.QueueCore
{
    public interface IMessageQueue<T> where T : IQueueMessage
    {
        Task<T?> DequeueMessageAsync(CancellationToken cancellationToken);

        void QueueMessage(T message);
    }
}
