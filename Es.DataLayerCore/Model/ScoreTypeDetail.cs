using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ScoreTypeDetail
    {
        public ScoreTypeDetail()
        {
            HomeworkAnswer = new HashSet<HomeworkAnswer>();
            ReportCardDetail = new HashSet<ReportCardDetail>();
            ResponseQuestion = new HashSet<ResponseQuestion>();
        }

        [Key]
        [Column("ScoreTypeDetailID")]
        public int ScoreTypeDetailId { get; set; }
        [Column("ScoreTypeID")]
        public int ScoreTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string ScoreTypeDetailTitle { get; set; }
        public int ScoreStart { get; set; }
        public int ScoreFinish { get; set; }

        [ForeignKey(nameof(ScoreTypeId))]
        [InverseProperty("ScoreTypeDetail")]
        public virtual ScoreType ScoreType { get; set; }
        [InverseProperty("HomeworkAnswerDescriptive")]
        public virtual ICollection<HomeworkAnswer> HomeworkAnswer { get; set; }
        [InverseProperty("ReportCardDescriptive")]
        public virtual ICollection<ReportCardDetail> ReportCardDetail { get; set; }
        [InverseProperty("ResponseQuestionDescriptive")]
        public virtual ICollection<ResponseQuestion> ResponseQuestion { get; set; }
    }
}
