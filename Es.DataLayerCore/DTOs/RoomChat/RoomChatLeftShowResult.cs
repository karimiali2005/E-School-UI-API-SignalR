using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Es.DataLayerCore.DTOs.RoomChat
{
    public partial class RoomChatLeftShowResult
    {
       
        public int RoomChatID { get; set; }
        public DateTime RoomChatDate { get; set; }
        public int SenderID { get; set; }
        public string SenderName { get; set; }
        public int? RecieverID { get; set; }
        public string RecieverName { get; set; }
        public int? RoomID { get; set; }
        public int? TeacherID { get; set; }
        public int? CourseID { get; set; }
        public string TextChat { get; set; }
        public string Filename { get; set; }
        public bool TagLearn { get; set; }
        public bool RoomChatDelete { get; set; }
        public DateTime? RoomChatUpdate { get; set; }
        public int? RoomChatParentID { get; set; }
        public bool AttachMsg { get; set; }
        public int? RoomChatGroupID { get; set; }
        public string ParentSenderName { get; set; }
        public string ParentTextChat { get; set; }
        public int? RoomChatViewNumber { get; set; }
        public string MimeType { get; set; }
        public string RoomChatFolder { get; set; }
        public bool RoomChatPlayVoice { get; set; }
    }

    public partial class RoomChatLeftShowResult
    {
        [NotMapped]
        public string RoomChatDateString {
            get
            {
                return RoomChatDate.ToString("HH:mm");
            }

        }
    }
}
