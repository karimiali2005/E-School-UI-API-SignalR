using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleAccess = new HashSet<RoleAccess>();
            UserRole = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<RoleAccess> RoleAccess { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
