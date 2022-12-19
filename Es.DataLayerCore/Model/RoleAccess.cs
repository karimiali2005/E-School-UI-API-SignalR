using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoleAccess
    {
        [Key]
        [Column("RoleAccessID")]
        public int RoleAccessId { get; set; }
        [Column("RoleID")]
        public int RoleId { get; set; }
        [Column("AccessoriesID")]
        public int AccessoriesId { get; set; }

        [ForeignKey(nameof(AccessoriesId))]
        [InverseProperty("RoleAccess")]
        public virtual Accessories Accessories { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("RoleAccess")]
        public virtual Role Role { get; set; }
    }
}
