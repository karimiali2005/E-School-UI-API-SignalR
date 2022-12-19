using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsServiceCore.DTOs
{

    public class vm_ScoreResponseByQuestionsAndChoices
    {
        public int? ResponseID { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public string ExamPic { get; set; }

        public bool HasNumberScore { get; set; }
        public int? QuestionCount { get; set; }
        public int? AnsweredQuestionCount { get; set; }
        public int? RestQuestionCount { get; set; }


        public int? ResponseQuestionID { get; set; }
        public string ResponseValue { get; set; }

        //نمره محاسباتی
        public decimal? ComputingResponseScore { get; set; }

        //نمره  عددی نهایی
        public decimal? ResponseScore { get; set; }
        [NotMapped]
        [RegularExpression(@"\d*(/\d{1,2})?", ErrorMessage = "نمره معتبر نیست.مثال:1/25")]
        public string ResponseScoreString { get; set; }

        // نمره نهایی طیفی
        public int? ResponseDescriptiveID { get; set; }
        
        //توضیحات معلم
        public string TeacherComment { get; set; }


        public string QuestionTeacherComment { get; set; }
        public decimal? ResponseQuestionScore { get; set; }
        [NotMapped]
        [RegularExpression(@"\d*(/\d{1,2})?", ErrorMessage = "نمره معتبر نیست")]
        public string ResponseQuestionScoreString { get; set; }

        public Question Question { get; set; }
        public int? NextQuestionId { get; set; }

        public List<ScoreTypeDetail> ScoreTypes { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
        public string DegreeTitle { get; set; }
        public string GradeTitle { get; set; }

        
        public string ErrorMsg { get; set; }
        public bool Status { get; set; }
    }

}
