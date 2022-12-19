using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChatMessageLast
    {
        [Column("RoomChatGroupID")]
        public int? RoomChatGroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RoomChatDate { get; set; }
        [Required]
        public string TextChat { get; set; }
        [StringLength(50)]
        public string Mimetype { get; set; }
        public long? Rank { get; set; }
    }
}
