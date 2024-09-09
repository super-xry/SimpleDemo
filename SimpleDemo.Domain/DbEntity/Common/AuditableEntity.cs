using System.ComponentModel.DataAnnotations;

namespace SimpleDemo.Domain.DbEntity.Common
{
    public class AuditableEntity : Entity, IAuditable
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        [StringLength(64)]
        public string CreatedBy { get; set; } = null!;

        [StringLength(64)]
        public string? ModifiedBy { get; set; }
    }
}