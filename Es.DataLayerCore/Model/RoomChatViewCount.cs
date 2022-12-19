using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChatViewCount
    {
        [Column("RoomChatID")]
        public int RoomChatId { get; set; }
        public int? RoomChatViewNumber { get; set; }
    }
}
