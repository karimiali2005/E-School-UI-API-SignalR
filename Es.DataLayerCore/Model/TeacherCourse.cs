using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class TeacherCourse
    {
        [Key]
        [Column("TeacherCourseID")]
        public int TeacherCourseId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Column("DegreeID")]
        public int? DegreeId { get; set; }
        [Column("GradeID")]
        public int? GradeId { get; set; }
        [Column("StudyFieldID")]
        public int? StudyFieldId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("TeacherCourse")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(DegreeId))]
        [InverseProperty("TeacherCourse")]
        public virtual Degree Degree { get; set; }
        [ForeignKey(nameof(GradeId))]
        [InverseProperty("TeacherCourse")]
        public virtual Grade Grade { get; set; }
        [ForeignKey(nameof(StudyFieldId))]
        [InverseProperty("TeacherCourse")]
        public virtual StudyField StudyField { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("TeacherCourse")]
        public virtual User User { get; set; }
    }
}
