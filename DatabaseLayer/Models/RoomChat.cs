using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoomChat
    {
        public RoomChat()
        {
            InverseRoomChatParent = new HashSet<RoomChat>();
        }

        public int RoomChatId { get; set; }
        public DateTime RoomChatDate { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int? RecieverId { get; set; }
        public string RecieverName { get; set; }
        public int RoomId { get; set; }
        public int TeacherId { get; set; }
        public int? CourseId { get; set; }
        public int? RoomChatGroupId { get; set; }
        public string TextChat { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }
        public bool TagLearn { get; set; }
        public bool RoomChatDelete { get; set; }
        public DateTime? RoomChatUpdate { get; set; }
        public int? RoomChatParentId { get; set; }
        public bool AttachMsg { get; set; }

        public virtual User Reciever { get; set; }
        public virtual Room Room { get; set; }
        public virtual RoomChat RoomChatParent { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Teacher { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<RoomChat> InverseRoomChatParent { get; set; }
    }
}
