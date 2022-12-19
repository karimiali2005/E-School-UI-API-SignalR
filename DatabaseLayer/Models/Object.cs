using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Object
    {
        public Object()
        {
            Accessories = new HashSet<Accessories>();
        }

        public int ObjectId { get; set; }
        public int GroupObjectId { get; set; }
        public string ObjectNameEn { get; set; }
        public string ObjectNameFa { get; set; }
        public int MyObjectOrder { get; set; }

        public virtual GroupObject GroupObject { get; set; }
        public virtual ICollection<Accessories> Accessories { get; set; }
    }
}
