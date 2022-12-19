using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ExamLoginType
    {
        public ExamLoginType()
        {
            Exam = new HashSet<Exam>();
        }

        [Key]
        [Column("ExamLoginTypeID")]
        public int ExamLoginTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string ExamLoginTypeTitle { get; set; }

        [InverseProperty("ExamLoginType")]
        public virtual ICollection<Exam> Exam { get; set; }
    }
}
