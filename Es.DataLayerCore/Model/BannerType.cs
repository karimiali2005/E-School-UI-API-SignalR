using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class BannerType
    {
        public BannerType()
        {
            Banner = new HashSet<Banner>();
        }

        [Key]
        [Column("BannerTypeID")]
        public int BannerTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string BannerTypeText { get; set; }

        [InverseProperty("BannerType")]
        public virtual ICollection<Banner> Banner { get; set; }
    }
}
