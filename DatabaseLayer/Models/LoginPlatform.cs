using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class LoginPlatform
    {
        public LoginPlatform()
        {
            Login = new HashSet<Login>();
        }

        public int LoginPlatformId { get; set; }
        public string LoginPlatformTitle { get; set; }

        public virtual ICollection<Login> Login { get; set; }
    }
}
