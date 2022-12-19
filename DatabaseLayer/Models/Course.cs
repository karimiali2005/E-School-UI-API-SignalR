using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Course
    {
        public Course()
        {
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            TeacherCourse = new HashSet<TeacherCourse>();
            RoomChat = new HashSet<RoomChat>();
        }

        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string ReportCardCourseTitle { get; set; }

        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<RoomChat> RoomChat { get; set; }
    }
}
