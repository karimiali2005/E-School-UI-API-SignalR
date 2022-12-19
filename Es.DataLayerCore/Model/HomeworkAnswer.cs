using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class HomeworkAnswer
    {
        public HomeworkAnswer()
        {
            HomeworkAnswerFile = new HashSet<HomeworkAnswerFile>();
        }

        [Key]
        [Column("HomeworkAnswerID")]
        public int HomeworkAnswerId { get; set; }
        [Column("HomeworkID")]
        public int HomeworkId { get; set; }
        [Column("HomeworkAnswerStatusID")]
        public int HomeworkAnswerStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisitDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EditDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SendDatetime { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public string HomeworkResponse { get; set; }
        public string HomeworkAnswerComment { get; set; }
        public int? HomeworkAnswerScore { get; set; }
        [Column("HomeworkAnswerDescriptiveID")]
        public int? HomeworkAnswerDescriptiveId { get; set; }
        public string TeacherComment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HomeworkAnswerSartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HomeworkAnswerFinishDate { get; set; }
        public bool HomeworkAnswerTeacherSee { get; set; }

        [ForeignKey(nameof(HomeworkId))]
        [InverseProperty("HomeworkAnswer")]
        public virtual Homework Homework { get; set; }
        [ForeignKey(nameof(HomeworkAnswerDescriptiveId))]
        [InverseProperty(nameof(ScoreTypeDetail.HomeworkAnswer))]
        public virtual ScoreTypeDetail HomeworkAnswerDescriptive { get; set; }
        [ForeignKey(nameof(HomeworkAnswerStatusId))]
        [InverseProperty("HomeworkAnswer")]
        public virtual HomeworkAnswerStatus HomeworkAnswerStatus { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("HomeworkAnswer")]
        public virtual User User { get; set; }
        [InverseProperty("HomeworkAnswer")]
        public virtual ICollection<HomeworkAnswerFile> HomeworkAnswerFile { get; set; }
    }
}
