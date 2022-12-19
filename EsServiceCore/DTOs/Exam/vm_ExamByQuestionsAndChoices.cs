using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.DTOs.Exam;
using Es.DataLayerCore.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EsServiceCore.DTOs
{
    
    public class vm_ExamByQuestionsAndChoices
    {
        public int? ExamId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "لطفا کلاس مورد نظر را انتخاب نمایید ")]
        public int RoomChatGroupId { get; set; }
        public int ExamLoginTypeId { get; set; }
        [Required(ErrorMessage = "لطفا عنوان آزمون را وارد نمایید ")]
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public string ExamPic { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile ExamPic_file { get; set; }


        [Required(ErrorMessage = "لطفا تاریخ شروع آزمون را وارد نمایید ")]
        public string ExamStartDateTime { get; set; }


        [Required(ErrorMessage = "لطفا ساعت شروع آزمون را وارد نمایید ")]
        public string ExamStartTime { get; set; }


        [Required(ErrorMessage = "لطفا ساعت پایان آزمون را وارد نمایید ")]
        public string ExamFinishTime { get; set; }


        [Required(ErrorMessage = "لطفا زمان تاخیر ورود به آزمون را وارد نمایید ")]
        public int DelayDeadline { get; set; }


        [Required(ErrorMessage = "لطفا زمان آزمون را وارد نمایید ")]
        public int ExamTime { get; set; }


        [Required(ErrorMessage = "لطفا تعداد ورود به آزمون را وارد نمایید ")]
        public int OpeningNumber { get; set; }

        public bool? HasRandomNumber { get; set; }
        public int RandomNumber { get; set; }

        [Required(ErrorMessage = "لطفا نوع نمره دهی را انتخاب نمایید ")]
        public int ScoreTypeId { get; set; }

        //public int Score { get; set; }
        public int Indexq { get; set; }
        public List<Question> Questions { get; set; }
        public List<ScoreTypesDetails> ScoreTypes { get; set; }
        public List<Select2Model> RoomChats { get; set; }
        //public List<QuestionFile> QuestionFiles { get; set; }
        //public List<QuestionChoiceFile> QuestionChoiceFiles { get; set; }
        public bool? ScoreIsNumber { get; set; }
        public string ErrorMsg { get; set; }
        public bool? HasResponse { get; set; }

    }

    //public class QuestionFile
    //{
    //    public int QuestionId { get; set; }

    //    [DataType(DataType.Upload)]
    //    public IFormFile QuestionFile_file { get; set; }

    //}
    //public class QuestionChoiceFile
    //{
    //    public int QuestionChoiceId { get; set; }
    //    [DataType(DataType.Upload)]
    //    public IFormFile QuestionChoiceFile_file { get; set; }


    //}
    //public partial class Question
    //{
    //    public List<QuestionChoice> QuestionChoices { get; set; }

    //}
}
