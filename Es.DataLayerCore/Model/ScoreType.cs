using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class ScoreType
    {
        public ScoreType()
        {
            Exam = new HashSet<Exam>();
            Homework = new HashSet<Homework>();
            ReportCard = new HashSet<ReportCard>();
            ScoreTypeDetail = new HashSet<ScoreTypeDetail>();
        }

        [Key]
        [Column("ScoreTypeID")]
        public int ScoreTypeId { get; set; }
        [StringLength(50)]
        public string ScoreTypeTitle { get; set; }
        public bool IsNumber { get; set; }

        [InverseProperty("ScoreType")]
        public virtual ICollection<Exam> Exam { get; set; }
        [InverseProperty("ScoreType")]
        public virtual ICollection<Homework> Homework { get; set; }
        [InverseProperty("ScoreType")]
        public virtual ICollection<ReportCard> ReportCard { get; set; }
        [InverseProperty("ScoreType")]
        public virtual ICollection<ScoreTypeDetail> ScoreTypeDetail { get; set; }
    }
}
