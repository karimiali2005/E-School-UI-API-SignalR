using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class HomeworkAnswerStatus
    {
        public HomeworkAnswerStatus()
        {
            HomeworkAnswer = new HashSet<HomeworkAnswer>();
        }

        [Key]
        [Column("HomeworkAnswerStatusID")]
        public int HomeworkAnswerStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string HomeworkAnswerStatusTitle { get; set; }

        [InverseProperty("HomeworkAnswerStatus")]
        public virtual ICollection<HomeworkAnswer> HomeworkAnswer { get; set; }
    }
}
