using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class BrowserType
    {
        [Key]
        [Column("BrowserTypeID")]
        [StringLength(50)]
        public string BrowserTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string BrowserTypeTitle { get; set; }
    }
}
