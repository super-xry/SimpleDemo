using Microsoft.Extensions.Logging;
using SimpleDemo.Shared.Enumerate;

namespace SimpleDemo.Infrastructure.Common
{
    public interface ILogService
    {
        Task LogInfoAsync(string title, string message, LogCategory category = LogCategory.Application, string? target = null);

        Task LogErrorAsync(string title, Exception ex, LogCategory category = LogCategory.Application, string? target = null);

        Task LogAsync(string title, string message, LogCategory category, LogLevel level, string? target = null);
    }
}