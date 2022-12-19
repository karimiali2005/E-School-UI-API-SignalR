using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class LoginPlatform
    {
        public LoginPlatform()
        {
            Login = new HashSet<Login>();
        }

        [Key]
        [Column("LoginPlatformID")]
        public int LoginPlatformId { get; set; }
        [Required]
        [StringLength(50)]
        public string LoginPlatformTitle { get; set; }

        [InverseProperty("LoginPlatform")]
        public virtual ICollection<Login> Login { get; set; }
    }
}
