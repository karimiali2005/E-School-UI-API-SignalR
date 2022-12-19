using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Response
    {
        public Response()
        {
            ResponseQuestion = new HashSet<ResponseQuestion>();
        }

        [Key]
        [Column("ResponseID")]
        public int ResponseId { get; set; }
        [Column("ExamID")]
        public int ExamId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FinishDateTime { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? ResponseScore { get; set; }
        [Column("ResponseDescriptiveID")]
        public int? ResponseDescriptiveId { get; set; }
        public string TeacherComment { get; set; }
        public int OpeningCount { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("Response")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Response")]
        public virtual User User { get; set; }
        [InverseProperty("Response")]
        public virtual ICollection<ResponseQuestion> ResponseQuestion { get; set; }
    }
}
