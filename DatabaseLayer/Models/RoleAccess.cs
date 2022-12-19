using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoleAccess
    {
        public int RoleAccessId { get; set; }
        public int RoleId { get; set; }
        public int AccessoriesId { get; set; }

        public virtual Accessories Accessories { get; set; }
        public virtual Role Role { get; set; }
    }
}
