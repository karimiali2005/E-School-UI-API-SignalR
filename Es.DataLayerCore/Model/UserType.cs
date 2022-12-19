using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class UserType
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("UserTypeID")]
        public int UserTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserTypeTitle { get; set; }

        [InverseProperty("UserType")]
        public virtual ICollection<User> User { get; set; }
    }
}
