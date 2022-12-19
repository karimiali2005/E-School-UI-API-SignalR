using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class CeremonyType
    {
        public CeremonyType()
        {
            Ceremony = new HashSet<Ceremony>();
        }

        [Key]
        [Column("CeremonyTypeID")]
        public int CeremonyTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string CeremonyTypeText { get; set; }

        [InverseProperty("CeremonyType")]
        public virtual ICollection<Ceremony> Ceremony { get; set; }
    }
}
