using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Question
    {
        public Question()
        {
            QuestionChoice = new HashSet<QuestionChoice>();
            ResponseQuestion = new HashSet<ResponseQuestion>();
        }

        [Key]
        [Column("QuestionID")]
        public int QuestionId { get; set; }
        [Column("ExamID")]
        public int ExamId { get; set; }
        [Column("QuestionTypeID")]
        public int QuestionTypeId { get; set; }
        [Required]
        [StringLength(1000)]
        public string QuestionTitle { get; set; }
        [StringLength(500)]
        public string QuestionFile { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRandomQuestion { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal QuestionScore { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("Question")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(QuestionTypeId))]
        [InverseProperty("Question")]
        public virtual QuestionType QuestionType { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<QuestionChoice> QuestionChoice { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<ResponseQuestion> ResponseQuestion { get; set; }
    }
}
