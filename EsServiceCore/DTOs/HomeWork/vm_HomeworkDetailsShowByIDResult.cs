using System;
using System.Collections.Generic;
using System.Text;

namespace EsServiceCore.DTOs.HomeWork
{
  public class vm_HomeworkDetailsShowByIDResult
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
        public List<FileAnswer> FileAnswers { get; set; }
       
        public int? ScoreTypeID { get; set; }
        public List<ScoreTypes> scoreTypes { get; set; }
        public bool IsNumber { get; set; }

    }
    public class FileAnswer
    {
        public int? HomeworkAnswerFileID { get; set; }
        public string FileName { get; set; }
    }
    public class ScoreTypes
    {
        public int id { get; set; }
        public string value { get; set; }
    }


}
