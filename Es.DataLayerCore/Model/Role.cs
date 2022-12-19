using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Role
    {
        public Role()
        {
            RoleAccess = new HashSet<RoleAccess>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RoleAccess> RoleAccess { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
