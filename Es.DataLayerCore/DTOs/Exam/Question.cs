using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
   public partial class Question
    {
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile QuestionFile_file { get; set; }

        [NotMapped]
        [RegularExpression(@"\d*(/\d{1,2})?", ErrorMessage = "نمره معتبر نیست.مثال:1/25")]
        public string QuestionScoreString { get; set; }
    }
}
