using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChatGroup
    {
        public RoomChatGroup()
        {
            Exam = new HashSet<Exam>();
            Homework = new HashSet<Homework>();
            ReportCardDetail = new HashSet<ReportCardDetail>();
            ReportCardTeacherCourse = new HashSet<ReportCardTeacherCourse>();
            RoomChat = new HashSet<RoomChat>();
            RoomChatView = new HashSet<RoomChatView>();
        }

        [Key]
        [Column("RoomChatGroupID")]
        public int RoomChatGroupId { get; set; }
        [StringLength(50)]
        public string RoomChatGroupTitle { get; set; }
        [Column("RoomID")]
        public int? RoomId { get; set; }
        [Column("TeacherID")]
        public int? TeacherId { get; set; }
        [Column("CourseID")]
        public int? CourseId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RoomChatGroupCreateDateTime { get; set; }
        public bool RoomChatGroupDelete { get; set; }
        public bool RoomChatGroupVisible { get; set; }
        [Column("StudentID")]
        public int? StudentId { get; set; }
        public int RoomChatGroupType { get; set; }
        public bool CloseChat { get; set; }
        public int RoomChatCount { get; set; }
        public int RoomChatLast { get; set; }
        public bool RoomChatGroupDeleteStudent { get; set; }
        public bool RoomChatGroupDeleteTeacher { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("RoomChatGroup")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("RoomChatGroup")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(User.RoomChatGroupStudent))]
        public virtual User Student { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty(nameof(User.RoomChatGroupTeacher))]
        public virtual User Teacher { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<Exam> Exam { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<Homework> Homework { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<ReportCardDetail> ReportCardDetail { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<ReportCardTeacherCourse> ReportCardTeacherCourse { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<RoomChat> RoomChat { get; set; }
        [InverseProperty("RoomChatGroup")]
        public virtual ICollection<RoomChatView> RoomChatView { get; set; }
    }
}
