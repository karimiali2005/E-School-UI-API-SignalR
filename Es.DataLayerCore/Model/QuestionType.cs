using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Question = new HashSet<Question>();
        }

        [Key]
        [Column("QuestionTypeID")]
        public int QuestionTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string QuestionTypeTitle { get; set; }

        [InverseProperty("QuestionType")]
        public virtual ICollection<Question> Question { get; set; }
    }
}
