using System;

namespace Es.DataLayerCore.DTOs.RoomChat
{
    public class RoomChatRightShowNewResult
    {
        public int RoomChatGroupID { get; set; }
        public int RoomChatGroupType { get; set; }
        public int? CourseID { get; set; }
        public int? TeacherID { get; set; }
        public int? RoomID { get; set; }
        public string RoomChatTitle { get; set; }
        public DateTime? RoomChatDate { get; set; }
        public string TextChat { get; set; }
        public string Mimetype { get; set; }
        public int MessageNewNumber { get; set; }
        public int HomeWorkNewNumber { get; set; }
        public string PicName { get; set; }
        public int UserIDPic { get; set; }
    }
}
