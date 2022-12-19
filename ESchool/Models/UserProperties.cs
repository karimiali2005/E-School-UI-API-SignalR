using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Models;

namespace ESchool.Models
{
    public class UserProperties
    {
        public IEnumerable<Degree> Degrees { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<StudyField> StudyFields { get; set; }

        public IEnumerable<UserType> UserTypes { get; set; }

        public IEnumerable<ParentStatus> ParentStatuses { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<DatabaseLayer.Models.User> Users { get; set; }

    }
}
