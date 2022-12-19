using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class View1
    {
        [Column("RoomChatID")]
        public int RoomChatId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public int? Expr1 { get; set; }
    }
}
