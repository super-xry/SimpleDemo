using SimpleDemo.Domain.DbEntity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDemo.Domain.DbEntity
{
    public class PermissionEntity : AuditableEntity
    {
        public string Label { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleEntity Role { get; set; } = null!;
    }
}