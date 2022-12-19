using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomDetail
    {
        [Key]
        [Column("RoomDetailID")]
        public int RoomDetailId { get; set; }
        [Column("RoomID")]
        public int RoomId { get; set; }
        [Column(TypeName = "date")]
        public DateTime RoomDetailDate { get; set; }
        [Required]
        [StringLength(5)]
        public string RoomDetailTimeStart { get; set; }
        [Required]
        [StringLength(5)]
        public string RoomDetailTimeFinish { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("CourseID")]
        public int? CourseId { get; set; }
        public string Comment { get; set; }
        [Required]
        public bool? CloseOnTime { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("RoomDetail")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("RoomDetail")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("RoomDetail")]
        public virtual User User { get; set; }
    }
}
