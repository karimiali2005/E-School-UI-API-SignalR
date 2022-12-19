using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class GroupObject
    {
        public GroupObject()
        {
            Object = new HashSet<Object>();
        }

        [Key]
        [Column("GroupObjectID")]
        public int GroupObjectId { get; set; }
        [Required]
        [StringLength(50)]
        public string GroupObjectName { get; set; }
        public int GroupObjectOrder { get; set; }

        [InverseProperty("GroupObject")]
        public virtual ICollection<Object> Object { get; set; }
    }
}
