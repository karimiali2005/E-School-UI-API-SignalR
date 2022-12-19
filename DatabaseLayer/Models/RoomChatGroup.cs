using System;

namespace DatabaseLayer.Models
{
    public partial class RoomChatGroup
    {
        public RoomChatGroup()
        {
           
        }

       
        public int RoomChatGroupId { get; set; }
      
        public string RoomChatGroupTitle { get; set; }
       
        public int? RoomId { get; set; }
       
        public int? TeacherId { get; set; }
     
        public int? CourseId { get; set; }
      
        public DateTime RoomChatGroupCreateDateTime { get; set; }
        public bool RoomChatGroupDelete { get; set; }
        public bool RoomChatGroupVisible { get; set; }


    }
}
