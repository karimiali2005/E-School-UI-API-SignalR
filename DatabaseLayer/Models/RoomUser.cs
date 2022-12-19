using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Models
{
    public partial class RoomUser
    {
        public int RoomUserId { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public bool IsOnline { get; set; }
        [StringLength(500)]
        public string ReportCardAddress { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
