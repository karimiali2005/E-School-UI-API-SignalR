using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Room = new HashSet<Room>();
            TeacherCourse = new HashSet<TeacherCourse>();
            User = new HashSet<User>();
        }

        public int GradeId { get; set; }
        public int DegreeId { get; set; }
        public string GradeTitle { get; set; }
        public bool HasStudyField { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
