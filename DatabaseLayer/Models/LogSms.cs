using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class LogSms
    {
        public int LogSmsId { get; set; }
        public DateTime CreateDate { get; set; }
        public string SendNumber { get; set; }
        public string MobileNumbers { get; set; }
        public string MessageText { get; set; }
        public string RecId { get; set; }
        public int LogSmsResultId { get; set; }
        public string Explain { get; set; }

        public virtual LogSmsResult LogSmsResult { get; set; }
    }
}
