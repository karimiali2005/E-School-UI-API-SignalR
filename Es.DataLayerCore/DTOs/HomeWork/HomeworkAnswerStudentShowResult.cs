using Es.DataLayerCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.HomeWork
{
    public class vm_HomeworkAnswerStudentShowResult
    {
        public List<HomeworkAnswerStudentShowResult> homeworkAnswers { get; set; }
        public int pageCount { get; set; }
    }
}
