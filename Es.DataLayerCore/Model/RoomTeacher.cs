using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomTeacher
    {
        [Key]
        [Column("RoomTeacherID")]
        public int RoomTeacherId { get; set; }
        [Column("RoomID")]
        public int RoomId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("CourseID")]
        public int? CourseId { get; set; }
        [StringLength(1000)]
        public string JitsiLiveAddress { get; set; }
        [StringLength(50)]
        public string JitsiLivePassword { get; set; }
        [StringLength(1000)]
        public string AdobeLiveAddress { get; set; }
        [StringLength(50)]
        public string AdobeLiveUsername { get; set; }
        [StringLength(50)]
        public string AdobeLivePass { get; set; }
        [StringLength(1000)]
        public string ExamAddress { get; set; }
        [StringLength(1000)]
        public string ZoomAddress { get; set; }
        [StringLength(1000)]
        public string ExamAddress2 { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("RoomTeacher")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("RoomTeacher")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("RoomTeacher")]
        public virtual User User { get; set; }
    }
}
