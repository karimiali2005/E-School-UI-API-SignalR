using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class UserPlatform
    {
        [Key]
        [Column("UserPlatformID")]
        public int UserPlatformId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(150)]
        public string PlatfornName { get; set; }
        [Required]
        [Column("mobileName")]
        [StringLength(150)]
        public string MobileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UserPlatformCreateDate { get; set; }
        [Required]
        [StringLength(500)]
        public string TokenFireBase { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserPlatform")]
        public virtual User User { get; set; }
    }
}
