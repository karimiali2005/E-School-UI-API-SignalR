using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Room
    {
        public Room()
        {
            RoomChat = new HashSet<RoomChat>();
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            RoomUser = new HashSet<RoomUser>();
        }

        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTitle { get; set; }
        public int? DegreeId { get; set; }
        public int? GradeId { get; set; }
        public int? StudyFieldId { get; set; }
        public DateTime RoomStartDate { get; set; }
        public DateTime RoomFinishDate { get; set; }
        public bool? CloseOnTime { get; set; }
        public bool CloseChat { get; set; }
        public bool PermissionCloseChat { get; set; }
        public bool PermissionStudentChatEdit { get; set; }
        public bool PermissionStudentChatDelete { get; set; }
        public int PinRoomChatId { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual ICollection<RoomChat> RoomChat { get; set; }
        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        public virtual ICollection<RoomUser> RoomUser { get; set; }
    }
}
