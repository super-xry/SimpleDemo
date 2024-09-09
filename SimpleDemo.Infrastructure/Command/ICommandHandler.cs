﻿namespace SimpleDemo.Infrastructure.Command
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}