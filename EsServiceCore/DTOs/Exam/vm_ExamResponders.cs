using Es.DataLayerCore.DTOs.Exam;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsServiceCore.DTOs
{
   public class vm_ExamResponders
    {
        public List<ExamResponseResult> Responses { get; set; }
        public List<ExamInfoResult> ExamInfo { get; set; }

        public string ErrorMsg { get; set; }
    }
}
