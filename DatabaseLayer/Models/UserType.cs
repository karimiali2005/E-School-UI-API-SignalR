using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class UserType
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeTitle { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
