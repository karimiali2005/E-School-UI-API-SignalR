using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class PreRegistration
    {
        [Key]
        [Column("PreRegistrationID")]
        public int PreRegistrationId { get; set; }
        [Required]
        [StringLength(50)]
        public string ClassRoom { get; set; }
        [Required]
        [Column("IRNational")]
        [StringLength(50)]
        public string Irnational { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string FatherName { get; set; }
        [Required]
        [StringLength(50)]
        public string FatherNumber { get; set; }
        [Required]
        [StringLength(150)]
        public string MotherFullName { get; set; }
        [Required]
        [StringLength(50)]
        public string MotherNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Pic { get; set; }
        public bool PreRegistrationArchive { get; set; }
    }
}
