using SimpleDemo.Domain.DbEntity.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleDemo.Domain.DbEntity
{
    public class RoleEntity : AuditableEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = null!;

        public IList<PermissionEntity> Permissions { get; set; } = [];
    }
}