using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Calendar
    {
        [Key]
        [Column("CalendarID")]
        public int CalendarId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CalendarDate { get; set; }
        public string CalendarComment { get; set; }
    }
}
