using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsServiceCore.DTOs.HomeWork
{
   public class vm_AnsverhomeWork
    {

       
      
       
        public int HomeworkAnswerId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int HomeworkId { get; set; }
        public int RoomId { get; set; }
        public int CourseId { get; set; }

        public string homeworktitel { get; set; }
        public string HomeWorkFileName { get; set; }
        public string HomeworkDescription { get; set; }
        public int HomeworkTypeID { get; set; }
        public int HomeworkAnswerStatusId { get; set; }      
        //پاسخ سوال در صورت متنی بودن
        public string HomeworkResponse { get; set; }
        public string HomeworkAnswerComment { get; set; }
        public int? HomeworkAnswerScore { get; set; }       
        public List<vmResponsFile> HomeworkResponseFile { get; set; }


    }
    public class vmResponsFile
    {
        public int id { get; set; }
        public string filename { get; set; }
    }
}
