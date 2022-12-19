using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
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

        [Key]
        [Column("DegreeID")]
        public int DegreeId { get; set; }
        [Required]
        [StringLength(50)]
        public string DegreeTitle { get; set; }
        public bool HasGrade { get; set; }

        [InverseProperty("Degree")]
        public virtual ICollection<Grade> Grade { get; set; }
        [InverseProperty("Degree")]
        public virtual ICollection<Room> Room { get; set; }
        [InverseProperty("Degree")]
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        [InverseProperty("Degree")]
        public virtual ICollection<User> User { get; set; }
    }
}
