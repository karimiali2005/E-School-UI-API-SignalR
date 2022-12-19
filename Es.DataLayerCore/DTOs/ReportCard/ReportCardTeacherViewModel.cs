using System.Collections.Generic;

namespace Es.DataLayerCore.DTOs.ReportCard
{
    public class ReportCardTeacherViewModel
    {
        public List<ReportCardTeacherCourseByRoomChatGroupIDResult> ReportCardTeacherCourseByRoomChatGroupIDResult { get; set; }
        public List<ReportCardTeacherShowResult> ReportCardTeacherShowResult { get; set; }
        public int RoomChatGroupID { get; set; }
    }
}
