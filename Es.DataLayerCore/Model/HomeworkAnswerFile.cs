using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class HomeworkAnswerFile
    {
        [Key]
        [Column("HomeworkAnswerFileID")]
        public int HomeworkAnswerFileId { get; set; }
        [Column("HomeworkAnswerID")]
        public int HomeworkAnswerId { get; set; }
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [ForeignKey(nameof(HomeworkAnswerId))]
        [InverseProperty("HomeworkAnswerFile")]
        public virtual HomeworkAnswer HomeworkAnswer { get; set; }
    }
}
