using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EsServiceCore.DTOs
{

    public class vm_ResponseByQuestionsAndChoices
    {
        public int? ResponseID { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public string ExamPic { get; set; }
        public string ExamStartDateTime { get; set; }
        public string ExamStartTime { get; set; }
        public string ExamFinishTime { get; set; }
        public string RestExamTime { get; set; }
        public int RestExamTimeSeconds { get; set; }
        public bool HasNumberScore { get; set; }
        public int? QuestionCount { get; set; }
        public int? AnsweredQuestionCount { get; set; }
        public int? RestQuestionCount { get; set; }


        public int? ResponseQuestionID { get; set; }
        public string ResponseValue { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ResponseValue_file { get; set; }


        public Question Question { get; set; }
        public int? NextQuestionId { get; set; }
        public string ErrorMsg { get; set; }
        public bool Status { get; set; }

        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }


    }

}
