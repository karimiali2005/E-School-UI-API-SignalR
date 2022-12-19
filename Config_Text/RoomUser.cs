using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLayer.Model
{
    public partial class RoomUser
    {
        [Key]
        [Column("RoomUserID")]
        public int RoomUserId { get; set; }
        [Column("RoomID")]
        public int RoomId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public bool IsOnline { get; set; }
        [StringLength(500)]
        public string ReportCardAddress { get; set; }

        [ForeignKey(nameof(RoomId))]
        [InverseProperty("RoomUser")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("RoomUser")]
        public virtual User User { get; set; }
    }
}
