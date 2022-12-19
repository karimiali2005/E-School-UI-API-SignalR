using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Introduction
    {
        [Key]
        [Column("IntroductionID")]
        public int IntroductionId { get; set; }
        [Required]
        [StringLength(50)]
        public string IntroductionPic { get; set; }
        [Column("IntroductionTypeID")]
        public int IntroductionTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string IntroductionTitle { get; set; }
        [Required]
        [StringLength(100)]
        public string IntroductionPosition { get; set; }

        [ForeignKey(nameof(IntroductionTypeId))]
        [InverseProperty("Introduction")]
        public virtual IntroductionType IntroductionType { get; set; }
    }
}
