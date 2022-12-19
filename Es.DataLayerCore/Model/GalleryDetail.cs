using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class GalleryDetail
    {
        [Key]
        [Column("GalleryDetailID")]
        public int GalleryDetailId { get; set; }
        [Column("GalleryID")]
        public int GalleryId { get; set; }
        [Required]
        [StringLength(50)]
        public string GalleryDetailName { get; set; }
        public bool GalleryIsDefault { get; set; }
        public int GalleryDetailType { get; set; }

        [ForeignKey(nameof(GalleryId))]
        [InverseProperty("GalleryDetail")]
        public virtual Gallery Gallery { get; set; }
    }
}
