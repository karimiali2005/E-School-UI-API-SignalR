using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Ceremony
    {
        [Key]
        [Column("CeremonyID")]
        public int CeremonyId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CeremonyDate { get; set; }
        [Column("CeremonyTypeID")]
        public int CeremonyTypeId { get; set; }
        [Required]
        [StringLength(1000)]
        public string CeremonyText { get; set; }
        [Column("RoomChatID")]
        public int? RoomChatId { get; set; }

        [ForeignKey(nameof(CeremonyTypeId))]
        [InverseProperty("Ceremony")]
        public virtual CeremonyType CeremonyType { get; set; }
        [ForeignKey(nameof(RoomChatId))]
        [InverseProperty("Ceremony")]
        public virtual RoomChat RoomChat { get; set; }
    }
}
