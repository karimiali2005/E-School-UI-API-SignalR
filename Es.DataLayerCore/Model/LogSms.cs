using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class LogSms
    {
        [Key]
        public int LogSmsId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(20)]
        public string SendNumber { get; set; }
        [Required]
        public string MobileNumbers { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public string RecId { get; set; }
        public int LogSmsResultId { get; set; }
        [StringLength(10)]
        public string Explain { get; set; }

        [ForeignKey(nameof(LogSmsResultId))]
        [InverseProperty("LogSms")]
        public virtual LogSmsResult LogSmsResult { get; set; }
    }
}
