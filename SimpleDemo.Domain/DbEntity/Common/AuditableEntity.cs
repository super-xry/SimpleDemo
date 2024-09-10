using System.ComponentModel.DataAnnotations;

namespace SimpleDemo.Domain.DbEntity.Common
{
    public class AuditableEntity : Entity, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedDate { get; set; }

        [StringLength(64)]
        public string CreatedBy { get; set; } = null!;

        [StringLength(64)]
        public string? ModifiedBy { get; set; }
    }
}