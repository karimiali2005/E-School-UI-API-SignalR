using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Gallery
    {
        public Gallery()
        {
            GalleryDetail = new HashSet<GalleryDetail>();
        }

        [Key]
        [Column("GalleryID")]
        public int GalleryId { get; set; }
        [Required]
        [StringLength(100)]
        public string GalleryTitle { get; set; }
        [StringLength(1000)]
        public string GalleryDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime GalleryDateCreate { get; set; }

        [InverseProperty("Gallery")]
        public virtual ICollection<GalleryDetail> GalleryDetail { get; set; }
    }
}
