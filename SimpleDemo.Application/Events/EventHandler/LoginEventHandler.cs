using Microsoft.Extensions.Logging;
using SimpleDemo.Domain.DbEntity;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Infrastructure.Events;
using SimpleDemo.Shared.Enumerate;

namespace SimpleDemo.Application.Events.EventHandler
{
    public class LoginEventHandler(ILogger<LoginEventHandler> logger, Func<ICommerceRepository> repositoryFactory) : IEventHandler<LoginEvent>
    {
        public async Task HandleAsync(LoginEvent @event)
        {
            // Todo: do something here.
            var message = $"Time: {@event.TimeStamp}, From:{@event.From}, System user id:{@event.UserId}";
            logger.LogInformation(message);

            using var repository = repositoryFactory();
            repository.Add(new LogEntity()
            {
                Message = message,
                Category = LogCategory.Application,
                Level = LogLevel.Information,
                Title = "User Login Event",
                CreatedBy = Shared.Constant.Common.DefaultCreator,
                Target = $"Login Successful: {@event.UserId != null && @event.UserId != Guid.Empty}"
            });
            await repository.UnitOfWork.CommitAsync(default);
        }
    }
}