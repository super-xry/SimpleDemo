namespace SimpleDemo.Infrastructure.QueueCore
{
    public interface IQueueMessage
    {
        public Guid Sequence { get;}
    }
}
