using SimpleDemo.Domain.DbEntity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDemo.Domain.DbEntity.CommerceEntity
{
    public class UserEntity : AuditableEntity
    {
        public string Name { get; set; } = null!;

        public string NickName { get; set; } = null!;

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleEntity Role { get; set; } = null!;
    }
}