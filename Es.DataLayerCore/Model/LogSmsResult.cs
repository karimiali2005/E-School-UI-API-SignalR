using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class LogSmsResult
    {
        public LogSmsResult()
        {
            LogSms = new HashSet<LogSms>();
        }

        [Key]
        public int LogSmsResultId { get; set; }
        [Required]
        public string LogSmsResultText { get; set; }

        [InverseProperty("LogSmsResult")]
        public virtual ICollection<LogSms> LogSms { get; set; }
    }
}
