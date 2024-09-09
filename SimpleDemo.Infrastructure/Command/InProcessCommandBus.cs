namespace SimpleDemo.Infrastructure.Command
{
    public class InProcessCommandBus(Func<Type, ICommandHandler> getCommandHandler) : ICommandBus
    {
        public virtual async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            var commandHandler = (ICommandHandler<TCommand>)getCommandHandler(typeof(ICommandHandler<TCommand>));

            if (commandHandler == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await commandHandler.HandleAsync(command, cancellationToken);
        }
    }
}