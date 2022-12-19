using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Grade
    {
        public Grade()
        {
            Room = new HashSet<Room>();
            TeacherCourse = new HashSet<TeacherCourse>();
            User = new HashSet<User>();
        }

        [Key]
        [Column("GradeID")]
        public int GradeId { get; set; }
        [Column("DegreeID")]
        public int DegreeId { get; set; }
        [Required]
        [StringLength(50)]
        public string GradeTitle { get; set; }
        public bool HasStudyField { get; set; }

        [ForeignKey(nameof(DegreeId))]
        [InverseProperty("Grade")]
        public virtual Degree Degree { get; set; }
        [InverseProperty("Grade")]
        public virtual ICollection<Room> Room { get; set; }
        [InverseProperty("Grade")]
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        [InverseProperty("Grade")]
        public virtual ICollection<User> User { get; set; }
    }
}
