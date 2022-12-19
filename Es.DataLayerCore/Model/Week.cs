using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Week
    {
        [Key]
        [Column("WeekID")]
        public int WeekId { get; set; }
        [Required]
        [StringLength(50)]
        public string WeekTitle { get; set; }
    }
}
