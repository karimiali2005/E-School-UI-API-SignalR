using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ParentStatus
    {
        public ParentStatus()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("ParentStatusID")]
        public int ParentStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string ParentStatusTitle { get; set; }

        [InverseProperty("ParentStatus")]
        public virtual ICollection<User> User { get; set; }
    }
}
