using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class LogSmsResult
    {
        public LogSmsResult()
        {
            LogSms = new HashSet<LogSms>();
        }

        public int LogSmsResultId { get; set; }
        public string LogSmsResultText { get; set; }

        public virtual ICollection<LogSms> LogSms { get; set; }
    }
}
