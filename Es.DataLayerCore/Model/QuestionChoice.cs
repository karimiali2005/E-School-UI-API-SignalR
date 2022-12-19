using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class QuestionChoice
    {
        [Key]
        [Column("QuestionChoiceID")]
        public int QuestionChoiceId { get; set; }
        [Column("QuestionID")]
        public int QuestionId { get; set; }
        [Required]
        [StringLength(500)]
        public string QuestionChoiceTitle { get; set; }
        [StringLength(50)]
        public string QuestionChoiceFile { get; set; }
        public bool IsAnswer { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("QuestionChoice")]
        public virtual Question Question { get; set; }
    }
}
