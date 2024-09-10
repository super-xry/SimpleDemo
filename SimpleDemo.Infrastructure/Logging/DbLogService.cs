using Microsoft.Extensions.Logging;
using SimpleDemo.Domain.DbEntity;
using SimpleDemo.Infrastructure.Common;
using SimpleDemo.Shared.Enumerate;

namespace SimpleDemo.Infrastructure.Logging
{
    public class DbLogService<T>(Func<T> repositoryFactory) : ILogService where T : ISimpleDemoRepository
    {
        public async Task LogInfoAsync(string title, string message, LogCategory category = LogCategory.Application, string? target = null)
        {
            await LogAsync(title, message, category, LogLevel.Error, target);
        }

        public async Task LogErrorAsync(string title, Exception ex, LogCategory category = LogCategory.Application, string? target = null)
        {
            await LogAsync(title, ex.Message, category, LogLevel.Error, target ?? ex.StackTrace);
        }

        public async Task LogAsync(string title, string message, LogCategory category, LogLevel level, string? target = null)
        {
            using var repository = repositoryFactory();

            var logEntity = new LogEntity()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Message = message,
                Category = category,
                Level = level,
                Target = target,
                CreatedBy = Shared.Constant.Common.DefaultCreator,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.Add(logEntity);

            await repository.UnitOfWork.CommitAsync(CancellationToken.None);
        }
    }
}