using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ResponseQuestion
    {
        [Key]
        [Column("ResponseQuestionID")]
        public int ResponseQuestionId { get; set; }
        [Column("ResponseID")]
        public int ResponseId { get; set; }
        [Column("QuestionID")]
        public int QuestionId { get; set; }
        [Required]
        public string ResponseValue { get; set; }
        [StringLength(1000)]
        public string QuestionTeacherComment { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? ResponseQuestionScore { get; set; }
        [Column("ResponseQuestionDescriptiveID")]
        public int? ResponseQuestionDescriptiveId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("ResponseQuestion")]
        public virtual Question Question { get; set; }
        [ForeignKey(nameof(ResponseId))]
        [InverseProperty("ResponseQuestion")]
        public virtual Response Response { get; set; }
        [ForeignKey(nameof(ResponseQuestionDescriptiveId))]
        [InverseProperty(nameof(ScoreTypeDetail.ResponseQuestion))]
        public virtual ScoreTypeDetail ResponseQuestionDescriptive { get; set; }
    }
}
