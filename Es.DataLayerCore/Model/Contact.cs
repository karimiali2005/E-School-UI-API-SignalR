using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Contact
    {
        [Key]
        [Column("ContactID")]
        public int ContactId { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Comment { get; set; }
        public bool ContactView { get; set; }
    }
}
