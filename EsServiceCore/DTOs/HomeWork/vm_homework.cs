using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsServiceCore.DTOs.HomeWork
{
   public class vm_homework
    {
      
        public int HomeworkId { get; set; }
        public string namedars { get; set; }
        public string HomeWorkFileName { get; set; }
        public IFormFile HomeWorkFileName_file { get; set; }

        [Required(ErrorMessage ="اشکال در  ارسال لطفا بازگشت بزنید و دوباره امتحان کنید ")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "اشکال در  ارسال لطفا بازگشت بزنید و دوباره امتحان کنید ")]
        public int CourseId { get; set; }
        [Required(ErrorMessage ="عنوان تکلیف الزامی است ")]
        [StringLength(200)]
        public string HomeworkTile { get; set; }
        public string HomeworkDescription { get; set; }
        [Required (ErrorMessage ="نوع فایل پاسوخ الزامی است")]
        public int HomeworkTypeId { get; set; }
        [Required(ErrorMessage ="نوع نمره الزامی است")]
        public int? ScoreTypeId { get; set; }

        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public string start { get; set; }
        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        public string end { get; set; }
    }
}
