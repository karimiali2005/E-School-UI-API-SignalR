using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomTeacherExam
    {
        [StringLength(1000)]
        public string ExamAddress { get; set; }
        [Column("RoomID")]
        public int RoomId { get; set; }
        public int ExamNumber { get; set; }
    }
}
