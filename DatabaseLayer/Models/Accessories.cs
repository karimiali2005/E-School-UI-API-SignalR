using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Accessories
    {
        public Accessories()
        {
            RoleAccess = new HashSet<RoleAccess>();
        }

        public int AccessoriesId { get; set; }
        public int ObjectId { get; set; }
        public string AccessoriesNameFa { get; set; }
        public string AccessoriesNameEn { get; set; }

        public virtual Object Object { get; set; }
        public virtual ICollection<RoleAccess> RoleAccess { get; set; }
    }
}
