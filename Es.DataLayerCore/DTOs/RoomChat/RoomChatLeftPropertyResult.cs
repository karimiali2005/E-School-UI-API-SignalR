using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.DTOs.RoomChat
{
    public partial class RoomChatLeftPropertyResult
    {
        public bool CloseOnTime { get; set; }
        public bool CloseChat { get; set; }
        public bool PermissionCloseChat { get; set; }
        public bool PermissionStudentChatEdit { get; set; }
        public bool PermissionStudentChatDelete { get; set; }
        public string JitsiLiveAddress { get; set; }
        public string JitsiLivePassword { get; set; }
        public string AdobeLiveAddress { get; set; }
        public string AdobeLiveUsername { get; set; }
        public string AdobeLivePass { get; set; }
        public string ExamAddress { get; set; }
        public int? PinRoomChatID { get; set; }
        public string PinTextChat { get; set; }
    }
    public partial class RoomChatLeftPropertyResult
    {
        [NotMapped]
        public int? CourseID { get; set; }
        [NotMapped]
        public int? RoomID { get; set; }
        [NotMapped]
        public int TeacherID { get; set; }
        [NotMapped]
        public int RoomChatGroupID { get; set; }
        [NotMapped]
        public string PicName { get; set; }
        [NotMapped]
        public string RoomChatGroupTitle { get; set; }
        [NotMapped]
        public int RoomChatGroupType { get; set; }
        [NotMapped]
        public int? RoomChatViewLast { get; set; }

    }
}