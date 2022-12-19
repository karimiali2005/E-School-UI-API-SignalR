using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class QuestionChoice
    {
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile QuestionChoiceFile_file { get; set; }
    }
}
