using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class GroupObject
    {
        public GroupObject()
        {
            Object = new HashSet<Object>();
        }

        public int GroupObjectId { get; set; }
        public string GroupObjectName { get; set; }
        public int GroupObjectOrder { get; set; }

        public virtual ICollection<Object> Object { get; set; }
    }
}
