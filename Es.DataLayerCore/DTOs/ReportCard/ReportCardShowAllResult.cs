using System;

namespace Es.DataLayerCore.DTOs.ReportCard
{
    public class ReportCardShowAllResult
    {
        public int ReportCardID { get; set; }
        public int GradeID { get; set; }
        public string ReportCardTitle { get; set; }
        public DateTime ReportCardDateTimeStart { get; set; }
        public DateTime ReportCardDateTimeFinish { get; set; }
        public bool ReportCardShowUser { get; set; }
        public bool ReportCardScoreEnable { get; set; }
        public int? ScoreTypeID { get; set; }
        public bool ReportCardAuto { get; set; }
        public string GradeTitle { get; set; }
        public string ScoreTypeTitle { get; set; }
    }
}
