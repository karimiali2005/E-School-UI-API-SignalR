using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Company
    {
        public Company()
        {
            Exam = new HashSet<Exam>();
        }

        [Key]
        [Column("CompanyID")]
        public int CompanyId { get; set; }
        [Required]
        [StringLength(100)]
        public string ComanyTtile { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Exam> Exam { get; set; }
    }
}
