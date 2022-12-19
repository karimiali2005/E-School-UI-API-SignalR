using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.Exam
{
    public class ExamResponseResult
    {
        public int ResponseID { get; set; }
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
        public int? RightNumber { get; set; }
        public int? WrongNumber { get; set; }
        public decimal? ResponseScore { get; set; }
        public decimal? CalculateScore { get; set; }
    }
}
