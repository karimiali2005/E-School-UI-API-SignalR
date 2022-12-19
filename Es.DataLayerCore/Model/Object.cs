using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Object
    {
        public Object()
        {
            Accessories = new HashSet<Accessories>();
        }

        [Key]
        [Column("ObjectID")]
        public int ObjectId { get; set; }
        [Column("GroupObjectID")]
        public int GroupObjectId { get; set; }
        [Required]
        [StringLength(50)]
        public string ObjectNameEn { get; set; }
        [Required]
        [StringLength(50)]
        public string ObjectNameFa { get; set; }
        public int MyObjectOrder { get; set; }

        [ForeignKey(nameof(GroupObjectId))]
        [InverseProperty("Object")]
        public virtual GroupObject GroupObject { get; set; }
        [InverseProperty("Object")]
        public virtual ICollection<Accessories> Accessories { get; set; }
    }
}
