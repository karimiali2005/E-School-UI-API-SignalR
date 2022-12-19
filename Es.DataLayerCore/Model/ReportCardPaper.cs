using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ReportCardPaper
    {
        [Key]
        [Column("ReportCardPaperID")]
        public int ReportCardPaperId { get; set; }
        [Column("ReportCardID")]
        public int ReportCardId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string ReportCardPaperFileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReportCardPaperDateTime { get; set; }

        [ForeignKey(nameof(ReportCardId))]
        [InverseProperty("ReportCardPaper")]
        public virtual ReportCard ReportCard { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ReportCardPaper")]
        public virtual User User { get; set; }
    }
}
