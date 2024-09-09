using Microsoft.Extensions.Logging;
using SimpleDemo.Domain.DbEntity.Common;
using SimpleDemo.Shared.Enumerate;

namespace SimpleDemo.Domain.DbEntity
{
    public class LogEntity : AuditableEntity
    {
        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public LogLevel Level { get; set; }

        public string? Target { get; set; }

        public LogCategory Category { get; set; }
    }
}
