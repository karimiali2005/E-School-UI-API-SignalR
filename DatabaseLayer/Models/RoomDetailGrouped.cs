using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class RoomDetailGrouped
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        
        public string DateStr { get; set; }
        public string DayStr { get; set; }
        public string TimeStr { get; set; }        
        public string CourseName { get; set; }        
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string Comment { get; set; }


    }
}
