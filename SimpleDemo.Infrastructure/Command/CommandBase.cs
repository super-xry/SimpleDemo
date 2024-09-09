namespace SimpleDemo.Infrastructure.Command
{
    public abstract class CommandBase : ICommand
    {
    }

    public abstract class CommandBase<TCommandResult> : ICommand<TCommandResult>
    {
        public TCommandResult? CommandResult { get; set; }
    }
}