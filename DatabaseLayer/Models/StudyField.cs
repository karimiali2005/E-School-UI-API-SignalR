using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class StudyField
    {
        public StudyField()
        {
            Room = new HashSet<Room>();
            TeacherCourse = new HashSet<TeacherCourse>();
            User = new HashSet<User>();
        }

        public int StudyFieldId { get; set; }
        public string StudyFieldTitle { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
