using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class ParentStatus
    {
        public ParentStatus()
        {
            User = new HashSet<User>();
        }

        public int ParentStatusId { get; set; }
        public string ParentStatusTitle { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
