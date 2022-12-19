namespace Es.DataLayerCore.DTOs.ReportCard
{
    public class ReportCardTeacherScoreShowResult
    {
        public int ReportCardID { get; set; }
        public string ReportCardTitle { get; set; }
        public int? ScoreTypeID { get; set; }
        public int UserID { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
        public decimal? ReportCardScore { get; set; }
        public int? ReportCardDescriptiveID { get; set; }
        public string ReportCardTeacherComment { get; set; }
        public int? ReportCardDetailID { get; set; }
        public bool ReportCardScoreEnable { get; set; }
    }
}
