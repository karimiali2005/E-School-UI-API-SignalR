using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class HomeworkType
    {
        public HomeworkType()
        {
            Homework = new HashSet<Homework>();
        }

        [Key]
        [Column("HomeworkTypeID")]
        public int HomeworkTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string HomeworkTypeTitle { get; set; }

        [InverseProperty("HomeworkType")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
