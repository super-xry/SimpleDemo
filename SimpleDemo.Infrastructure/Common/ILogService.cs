using Microsoft.Extensions.Logging;
using SimpleDemo.Shared.Enumerate;

namespace SimpleDemo.Infrastructure.Common
{
    public interface ILogService<T> where T : class
    {
        Task LogInfoAsync(string code, string message, LogCategory category = LogCategory.Application, string? target = null);

        Task LogErrorAsync(string code, Exception ex, LogCategory category = LogCategory.Application, string? target = null);

        Task LogAsync(string code, string message, LogCategory category, LogLevel level, string? target = null);
    }
}