using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class DisciplineType
    {
        public DisciplineType()
        {
            Discipline = new HashSet<Discipline>();
        }

        [Key]
        [Column("DisciplineTypeID")]
        public int DisciplineTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string DisciplineTypeText { get; set; }

        [InverseProperty("DisciplineType")]
        public virtual ICollection<Discipline> Discipline { get; set; }
    }
}
