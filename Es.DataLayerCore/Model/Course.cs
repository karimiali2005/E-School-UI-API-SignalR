using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
            ReportCardDetail = new HashSet<ReportCardDetail>();
            ReportCardTeacherCourse = new HashSet<ReportCardTeacherCourse>();
            RoomChat = new HashSet<RoomChat>();
            RoomChatGroup = new HashSet<RoomChatGroup>();
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            TeacherCourse = new HashSet<TeacherCourse>();
        }

        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Required]
        [StringLength(50)]
        public string CourseTitle { get; set; }
        [StringLength(50)]
        public string ReportCardCourseTitle { get; set; }

        [InverseProperty("Course")]
        public virtual ICollection<Homework> Homework { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<ReportCardDetail> ReportCardDetail { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<ReportCardTeacherCourse> ReportCardTeacherCourse { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<RoomChat> RoomChat { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<RoomChatGroup> RoomChatGroup { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
