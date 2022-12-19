using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChatViewInfo
    {
        [Column("UserID")]
        public int UserId { get; set; }
        public int MessageViewNumber { get; set; }
        [Column("RoomChatGroupID")]
        public int? RoomChatGroupId { get; set; }
    }
}
