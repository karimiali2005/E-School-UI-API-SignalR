using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Versioning
    {
        [Key]
        [Column("VersioningID")]
        public int VersioningId { get; set; }
        public int VersionCode { get; set; }
        [Required]
        [StringLength(10)]
        public string VersionName { get; set; }
        public bool ForceInstalling { get; set; }
        [StringLength(200)]
        public string VersionDescription { get; set; }
        [StringLength(1000)]
        public string VersioningUrl { get; set; }
    }
}
