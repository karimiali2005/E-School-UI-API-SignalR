using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ReportCardDetail
    {
        [Key]
        [Column("ReportCardDetailID")]
        public int ReportCardDetailId { get; set; }
        [Column("ReportCardID")]
        public int ReportCardId { get; set; }
        [Column("RoomChatGroupID")]
        public int RoomChatGroupId { get; set; }
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Column("TeacherID")]
        public int TeacherId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? ReportCardScore { get; set; }
        [Column("ReportCardDescriptiveID")]
        public int? ReportCardDescriptiveId { get; set; }
        [StringLength(500)]
        public string ReportCardTeacherComment { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("ReportCardDetail")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(ReportCardId))]
        [InverseProperty("ReportCardDetail")]
        public virtual ReportCard ReportCard { get; set; }
        [ForeignKey(nameof(ReportCardDescriptiveId))]
        [InverseProperty(nameof(ScoreTypeDetail.ReportCardDetail))]
        public virtual ScoreTypeDetail ReportCardDescriptive { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("ReportCardDetail")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty("ReportCardDetailTeacher")]
        public virtual User Teacher { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ReportCardDetailUser")]
        public virtual User User { get; set; }
    }
}
