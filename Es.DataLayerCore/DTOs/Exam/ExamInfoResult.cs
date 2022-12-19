using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.Exam
{
    public class ExamInfoResult
    {
        public int ExamID { get; set; }
        public DateTime ExamStartDateTime { get; set; }
        public int? ResponseNumber { get; set; }
        public int? StudentAll { get; set; }
    }
}
