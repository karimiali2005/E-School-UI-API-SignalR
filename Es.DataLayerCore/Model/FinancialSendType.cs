using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class FinancialSendType
    {
        public FinancialSendType()
        {
            Financial = new HashSet<Financial>();
        }

        [Key]
        [Column("FinancialSendTypeID")]
        public int FinancialSendTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string FinancialSendTypeText { get; set; }

        [InverseProperty("FinancialSendType")]
        public virtual ICollection<Financial> Financial { get; set; }
    }
}
