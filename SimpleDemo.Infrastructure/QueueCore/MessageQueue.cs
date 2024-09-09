using System.Collections.Concurrent;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;

namespace SimpleDemo.Infrastructure.QueueCore
{
    public class MessageQueue<T> : IMessageQueue<T> where T : IQueueMessage
    {
        private readonly ConcurrentQueue<T> _subscriberMessages = new();
        private readonly SemaphoreSlim _signal = new(0);

        public virtual async Task<T?> DequeueMessageAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);

            _subscriberMessages.TryDequeue(out var message);

            return message;
        }

        public virtual void QueueMessage(T message)
        {
            if (message == null)
            {
                throw new SimpleInvalidException(SimpleExceptionCode.Application.QueueMessageIsRequired, $"Queue message is required({typeof(T)}).");
            }

            if (_subscriberMessages.Any(it => it.Sequence == message.Sequence)) return;
            _subscriberMessages.Enqueue(message);
            _signal.Release();
        }
    }
}
