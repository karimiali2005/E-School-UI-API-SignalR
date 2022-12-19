using System;

namespace Es.DataLayerCore.DTOs.ReportCard
{
    public class ReportCardStudentShowResult
    {
        public int ReportCardID { get; set; }
        public string ReportCardTitle { get; set; }
        public DateTime ReportCardDateTimeStart { get; set; }
        public DateTime ReportCardDateTimeFinish { get; set; }
        public string ScoreTypeTitle { get; set; }
        public int? ReportCardDetailID { get; set; }
        public int? ReportCardPaperID { get; set; }
        public string ReportCardPaperFileName { get; set; }
        public string ReportCardPaperDateTime { get; set; }
    }

}
