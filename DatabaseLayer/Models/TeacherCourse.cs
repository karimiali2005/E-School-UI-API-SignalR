using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class TeacherCourse
    {
        public int TeacherCourseId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int? DegreeId { get; set; }
        public int? GradeId { get; set; }
        public int? StudyFieldId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual User User { get; set; }
    }
}
