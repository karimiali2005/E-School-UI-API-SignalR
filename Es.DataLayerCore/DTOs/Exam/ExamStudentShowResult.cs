using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.Exam
{
    public class ExamStudentShowResult
    {
        public int ExamID { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public string ExamPic { get; set; }
        public int CompanyID { get; set; }
        public int RoomChatGroupID { get; set; }
        public int ExamLoginTypeID { get; set; }
        public DateTime ExamCreateDateTime { get; set; }
        public DateTime ExamStartDateTime { get; set; }
        public DateTime ExamFinishDateTime { get; set; }
        public int DelayDeadline { get; set; }
        public int ExamTime { get; set; }
        public int OpeningNumber { get; set; }
        public int RandomNumber { get; set; }
        public bool? ExamOn { get; set; }
        public bool? ExamCancel { get; set; }
        public string ExamCancelReason { get; set; }
        public int ScoreTypeID { get; set; }
        public string RoomChatGroupTitle { get; set; }
        public int UserIDCreate { get; set; }
        public string FullNameCreate { get; set; }
        public int UserID { get; set; }
        public string CourseTitle { get; set; }
        public int? UserIDTeacher { get; set; }
        public string FullNameTeacher { get; set; }
        public int? ResponseID { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public decimal? ResponseScore { get; set; }
        public int? ResponseDescriptiveID { get; set; }
        public string TeacherComment { get; set; }
        public int? OpeningCount { get; set; }
    }
}
