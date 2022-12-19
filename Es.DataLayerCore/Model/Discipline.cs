using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Discipline
    {
        [Key]
        [Column("DisciplineID")]
        public int DisciplineId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DisciplineDate { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("DisciplineTypeID")]
        public int DisciplineTypeId { get; set; }
        [Required]
        [StringLength(1000)]
        public string DisciplineText { get; set; }
        [Column("RoomChatID")]
        public int? RoomChatId { get; set; }
        [Column("RoomChatID2")]
        public int? RoomChatId2 { get; set; }

        [ForeignKey(nameof(DisciplineTypeId))]
        [InverseProperty("Discipline")]
        public virtual DisciplineType DisciplineType { get; set; }
        [ForeignKey(nameof(RoomChatId))]
        [InverseProperty("DisciplineRoomChat")]
        public virtual RoomChat RoomChat { get; set; }
        [ForeignKey(nameof(RoomChatId2))]
        [InverseProperty("DisciplineRoomChatId2Navigation")]
        public virtual RoomChat RoomChatId2Navigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Discipline")]
        public virtual User User { get; set; }
    }
}
