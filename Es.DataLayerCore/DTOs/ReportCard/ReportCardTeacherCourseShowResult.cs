namespace Es.DataLayerCore.DTOs.ReportCard
{
    public class ReportCardTeacherCourseShowResult
    {
        public int ReportCardTeacherCourseID { get; set; }
        public int RoomChatGroupID { get; set; }
        public int CourseID { get; set; }
        public int? TeacherID { get; set; }
        public string RoomChatGroupTitle { get; set; }
        public string CourseTitle { get; set; }
        public string FullName { get; set; }
    }
}
