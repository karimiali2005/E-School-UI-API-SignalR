using Es.DataLayerCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs
{
    public class vm_HomeworkShowResult
    {
        public List<HomeworkShowResult>  homeworkShowResults { get; set; }
        public int pageCount { get; set; }
    }
}
