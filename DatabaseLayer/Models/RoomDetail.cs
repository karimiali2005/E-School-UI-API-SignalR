using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoomDetail
    {
        public int RoomDetailId { get; set; }
        public int RoomId { get; set; }
        public DateTime RoomDetailDate { get; set; }
        public string RoomDetailTimeStart { get; set; }
        public string RoomDetailTimeFinish { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }
        public string Comment { get; set; }
        public bool? CloseOnTime { get; set; }

        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
