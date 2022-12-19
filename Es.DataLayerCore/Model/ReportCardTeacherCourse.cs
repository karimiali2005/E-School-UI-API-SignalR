using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ReportCardTeacherCourse
    {
        [Key]
        [Column("ReportCardTeacherCourseID")]
        public int ReportCardTeacherCourseId { get; set; }
        [Column("ReportCardID")]
        public int ReportCardId { get; set; }
        [Column("RoomChatGroupID")]
        public int RoomChatGroupId { get; set; }
        [Column("CourseID")]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("ReportCardTeacherCourse")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(ReportCardId))]
        [InverseProperty("ReportCardTeacherCourse")]
        public virtual ReportCard ReportCard { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("ReportCardTeacherCourse")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
    }
}
