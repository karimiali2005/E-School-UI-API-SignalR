using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class IntroductionType
    {
        public IntroductionType()
        {
            Introduction = new HashSet<Introduction>();
        }

        [Key]
        [Column("IntroductionTypeID")]
        public int IntroductionTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string IntroductionTypeTitle { get; set; }

        [InverseProperty("IntroductionType")]
        public virtual ICollection<Introduction> Introduction { get; set; }
    }
}
