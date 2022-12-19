using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Banner
    {
        [Key]
        [Column("BannerID")]
        public int BannerId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BannerDate { get; set; }
        [Column("BannerTypeID")]
        public int BannerTypeId { get; set; }
        [Required]
        [StringLength(1000)]
        public string BannerText { get; set; }
        [Column("RoomChatID")]
        public int? RoomChatId { get; set; }

        [ForeignKey(nameof(BannerTypeId))]
        [InverseProperty("Banner")]
        public virtual BannerType BannerType { get; set; }
        [ForeignKey(nameof(RoomChatId))]
        [InverseProperty("Banner")]
        public virtual RoomChat RoomChat { get; set; }
    }
}
