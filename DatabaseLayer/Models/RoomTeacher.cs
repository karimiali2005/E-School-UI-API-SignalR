using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoomTeacher
    {
        public int RoomTeacherId { get; set; }
        public int RoomId { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }
        public string JitsiLiveAddress { get; set; }
        public string JitsiLivePassword { get; set; }
        public string AdobeLiveAddress { get; set; }
        public string AdobeLiveUsername { get; set; }
        public string AdobeLivePass { get; set; }
        public string ExamAddress { get; set; }
        public string ZoomAddress { get; set; }
        public string ExamAddress2 { get; set; }

        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
