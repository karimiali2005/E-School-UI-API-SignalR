using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChatView
    {
        [Key]
        [Column("RoomChatViewID")]
        public int RoomChatViewId { get; set; }
        [Column("RoomChatGroupID")]
        public int RoomChatGroupId { get; set; }
        [Column("RoomChatID")]
        public int RoomChatId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ViewDateTime { get; set; }
        public bool RoomChatViewDelete { get; set; }
        public bool RoomChatPlayVoice { get; set; }

        [ForeignKey(nameof(RoomChatId))]
        [InverseProperty("RoomChatView")]
        public virtual RoomChat RoomChat { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("RoomChatView")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("RoomChatView")]
        public virtual User User { get; set; }
    }
}
