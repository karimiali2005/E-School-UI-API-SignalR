using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class StudyField
    {
        public StudyField()
        {
            Room = new HashSet<Room>();
            TeacherCourse = new HashSet<TeacherCourse>();
            User = new HashSet<User>();
        }

        [Key]
        [Column("StudyFieldID")]
        public int StudyFieldId { get; set; }
        [Required]
        [StringLength(50)]
        public string StudyFieldTitle { get; set; }

        [InverseProperty("StudyField")]
        public virtual ICollection<Room> Room { get; set; }
        [InverseProperty("StudyField")]
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        [InverseProperty("StudyField")]
        public virtual ICollection<User> User { get; set; }
    }
}
