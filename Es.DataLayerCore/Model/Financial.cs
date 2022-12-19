using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Financial
    {
        [Key]
        [Column("FinancialID")]
        public int FinancialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FinancialDate { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("FinancialSendTypeID")]
        public int FinancialSendTypeId { get; set; }
        [Required]
        [StringLength(1000)]
        public string FinancialText { get; set; }
        [Column("RoomChatID")]
        public int? RoomChatId { get; set; }
        [Column("RoomChatID2")]
        public int? RoomChatId2 { get; set; }

        [ForeignKey(nameof(FinancialSendTypeId))]
        [InverseProperty("Financial")]
        public virtual FinancialSendType FinancialSendType { get; set; }
        [ForeignKey(nameof(RoomChatId))]
        [InverseProperty("FinancialRoomChat")]
        public virtual RoomChat RoomChat { get; set; }
        [ForeignKey(nameof(RoomChatId2))]
        [InverseProperty("FinancialRoomChatId2Navigation")]
        public virtual RoomChat RoomChatId2Navigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Financial")]
        public virtual User User { get; set; }
    }
}
