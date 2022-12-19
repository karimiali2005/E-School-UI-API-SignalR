using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Calendar
    {
        public int CalendarId { get; set; }
        public DateTime CalendarDate { get; set; }
        public string CalendarComment { get; set; }
    }
}
