namespace SimpleDemo.Infrastructure.Command
{
    public interface ICommand
    {
    }

    public interface ICommand<TCommandResult> : ICommand
    {
        TCommandResult? CommandResult { get; set; }
    }
}