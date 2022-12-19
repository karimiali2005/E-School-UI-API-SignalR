using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs
{
    public class HomeworkAnswerStudentShowResult
    {
        public int HomeworkID { get; set; }
        public string HomeworkTile { get; set; }
        public string HomeworkDescription { get; set; }
        public DateTime? HomeworkStartDate { get; set; }
        public DateTime? HomeworkFinishDate { get; set; }
        public int HomeworkTypeID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int RoomID { get; set; }
        public int HomeworkAnswerStatusID { get; set; }
        public string HomeworkAnswerStatusTitle { get; set; }
        public int HomeworkAnswerID { get; set; }
        public DateTime? VisitDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public DateTime? SendDatetime { get; set; }
        public int? ScoreTypeID { get; set; }
        public string HomeworkAnswerScore { get; set; }
        public string TeacherComment { get; set; }
        public int? RecordCount { get; set; }
    }

    public class HomeworkByIDResult
    {
        public int HomeworkID { get; set; }
        public int CourseID { get; set; }
        public string HomeworkTile { get; set; }
        public string HomeworkDescription { get; set; }
        public string HomeWorkFileName { get; set; }
        public int HomeworkTypeID { get; set; }
        public int? ScoreTypeID { get; set; }
        public int UserIdTeacher { get; set; }
        public int? HomeworkAnswerID { get; set; }
        public int? HomeworkAnswerStatusID { get; set; }
        public DateTime? VisitDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public DateTime? SendDatetime { get; set; }
        public DateTime? HomeworkAnswerSartDate { get; set; }
        public DateTime? HomeworkAnswerFinishDate { get; set; }
        public string TeacherComment { get; set; }
        public int? HomeworkAnswerDescriptiveID { get; set; }
        public int? HomeworkAnswerScore { get; set; }
        public string HomeworkAnswerComment { get; set; }
        public string HomeworkResponse { get; set; }
        public string FileName { get; set; }
        public int? HomeworkAnswerFileID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
    }

    public class HomeworkDetailShowResult
    {
        public int UserID { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
        public int HomeworkID { get; set; }
        public int? HomeworkAnswerID { get; set; }
        public int? HomeworkAnswerStatusID { get; set; }
        public DateTime? SendDatetime { get; set; }
        public DateTime? HomeworkAnswerSartDate { get; set; }
        public DateTime? HomeworkAnswerFinishDate { get; set; }
        public string TeacherComment { get; set; }
        public int? HomeworkAnswerScore { get; set; }
        public int? HomeworkAnswerDescriptiveID { get; set; }
        public string HomeworkResponse { get; set; }
        public string HomeworkAnswerComment { get; set; }
        public DateTime? VisitDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public string FinalScore { get; set; }
        public string HomeworkAnswerStatusTitle { get; set; }
        public bool HomeworkAnswerTeacherSee { get; set; }
    }

    public class HomeworkDetailsShowByIDResult
    {
        public int HomeworkAnswerID { get; set; }
        public int HomeworkAnswerStatusID { get; set; }
        public string HomeworkAnswerStatusTitle { get; set; }
        public DateTime? VisitDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public DateTime? SendDatetime { get; set; }
        public int UserID { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
        public string HomeworkResponse { get; set; }
        public string HomeworkAnswerComment { get; set; }
        public int? HomeworkAnswerScore { get; set; }
        public int? HomeworkAnswerDescriptiveID { get; set; }
        public string TeacherComment { get; set; }
        public DateTime? HomeworkAnswerSartDate { get; set; }
        public DateTime? HomeworkAnswerFinishDate { get; set; }
        public int? HomeworkAnswerFileID { get; set; }
        public string FileName { get; set; }
        public int? ScoreTypeID { get; set; }
        public bool IsNumber { get; set; }
    }

    public class HomeworkShowResult
    {
        public int HomeworkID { get; set; }
        public string HomeworkTile { get; set; }
        public string HomeworkDescription { get; set; }
        public int HomeworkTypeID { get; set; }
        public int ScoreTypeID { get; set; }
        public string ScoreTypeTitle { get; set; }
        public DateTime? HomeworkStartDate { get; set; }
        public DateTime? HomeworkFinishDate { get; set; }
        public int? StudentAllNumber { get; set; }
        public int StudentAnswerNumber { get; set; }
        public int? RecordCount { get; set; }
    }

    public class ScoreTypeDetailShowResult
    {
        public int ScoreTypeID { get; set; }
        public string ScoreTypeTitle { get; set; }
        public bool IsNumber { get; set; }
        public string SumScoreTypeDetailTitle { get; set; }
    }

}
