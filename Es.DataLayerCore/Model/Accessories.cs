using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Accessories
    {
        public Accessories()
        {
            RoleAccess = new HashSet<RoleAccess>();
        }

        [Key]
        [Column("AccessoriesID")]
        public int AccessoriesId { get; set; }
        [Column("ObjectID")]
        public int ObjectId { get; set; }
        [Required]
        [StringLength(150)]
        public string AccessoriesNameFa { get; set; }
        [Required]
        [StringLength(50)]
        public string AccessoriesNameEn { get; set; }

        [ForeignKey(nameof(ObjectId))]
        [InverseProperty("Accessories")]
        public virtual Object Object { get; set; }
        [InverseProperty("Accessories")]
        public virtual ICollection<RoleAccess> RoleAccess { get; set; }
    }
}
