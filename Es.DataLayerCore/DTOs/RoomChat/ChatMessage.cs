using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Es.DataLayerCore.DTOs.RoomChat
{
    public class ChatMessage
    {
        public string chatType { get; set; }
        public int groupId { get; set; }
        public int senderId { get; set; }
        public int roomChatId { get; set; }
        public string senderName { get; set; }
        public string textChat { get; set; }
        public string filename { get; set; }
        public string mimeType { get; set; }
        public int? roomChatParentId { get; set; }
        public string parentTextChat { get; set; }
        public string parentSenderName { get; set; }
        public bool tagLearn { get; set; }
        public bool attachMsg { get; set; }
        public string mainAddress { get; set; }
        public int roomChatViewNumber { get; set; }
        public string roomChatDateString { get; set; }
        public DateTime roomChatDate { get; set; }
        public string roomChatGroupTitle { get; set; }
        public int? roomChatGroupType { get; set; }
    }
}
