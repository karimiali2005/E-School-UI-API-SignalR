using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ReportCard
    {
        public ReportCard()
        {
            ReportCardDetail = new HashSet<ReportCardDetail>();
            ReportCardPaper = new HashSet<ReportCardPaper>();
            ReportCardTeacherCourse = new HashSet<ReportCardTeacherCourse>();
        }

        [Key]
        [Column("ReportCardID")]
        public int ReportCardId { get; set; }
        [Column("GradeID")]
        public int GradeId { get; set; }
        [Required]
        [StringLength(100)]
        public string ReportCardTitle { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReportCardDateTimeStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReportCardDateTimeFinish { get; set; }
        public bool ReportCardShowUser { get; set; }
        public bool ReportCardScoreEnable { get; set; }
        [Column("ScoreTypeID")]
        public int? ScoreTypeId { get; set; }
        public bool ReportCardAuto { get; set; }

        [ForeignKey(nameof(ScoreTypeId))]
        [InverseProperty("ReportCard")]
        public virtual ScoreType ScoreType { get; set; }
        [InverseProperty("ReportCard")]
        public virtual ICollection<ReportCardDetail> ReportCardDetail { get; set; }
        [InverseProperty("ReportCard")]
        public virtual ICollection<ReportCardPaper> ReportCardPaper { get; set; }
        [InverseProperty("ReportCard")]
        public virtual ICollection<ReportCardTeacherCourse> ReportCardTeacherCourse { get; set; }
    }
}
