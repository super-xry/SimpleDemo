namespace SimpleDemo.Infrastructure.Command
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
    }
}