using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class DeviceType
    {
        [Key]
        [Column("DeviceTypeID")]
        [StringLength(50)]
        public string DeviceTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string DeviceTypeTitle { get; set; }
    }
}
