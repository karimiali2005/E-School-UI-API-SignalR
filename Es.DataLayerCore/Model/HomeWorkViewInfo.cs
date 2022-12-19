using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class HomeWorkViewInfo
    {
        [Column("RoomChatGroupID")]
        public int? RoomChatGroupId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        public int? HomeWorkViewNumber { get; set; }
    }
}
