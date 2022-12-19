using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;


namespace ESchool.Models
{
    public class RoomProperties
    {
        public IEnumerable<RoomType> RoomTypes { get; set; }

        public IEnumerable<RoomUser> RoomUsers { get; set; }

        public List<Room> Rooms { get; set; }

        public IEnumerable<RoomDetail> RoomDetails { get; set; }

        public IEnumerable<RoomTeacher> RoomTeachers { get; set; }

        public IEnumerable<RoomChat> RoomChats { get; set; }

        public IEnumerable<Degree> Degrees { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<StudyField> StudyFields { get; set; }

        public List<Course> Courses { get; set; }

        public IEnumerable<UserType> UserTypes { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<UserCurrentViewModel> UserCurrentViewModels { get; set; }

        public IEnumerable<RoomChatGroup> RoomChatGroups { get; set; }


    }
}
