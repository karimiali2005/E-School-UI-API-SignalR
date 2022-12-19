using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Degree
    {
        public Degree()
        {
            Grade = new HashSet<Grade>();
            Room = new HashSet<Room>();
            TeacherCourse = new HashSet<TeacherCourse>();
            User = new HashSet<User>();
        }

        public int DegreeId { get; set; }
        public string DegreeTitle { get; set; }
        public bool HasGrade { get; set; }

        public virtual ICollection<Grade> Grade { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
