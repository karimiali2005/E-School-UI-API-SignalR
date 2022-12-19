using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Exam
    {
        public Exam()
        {
            Question = new HashSet<Question>();
            Response = new HashSet<Response>();
        }

        [Key]
        [Column("ExamID")]
        public int ExamId { get; set; }
        [Required]
        [StringLength(250)]
        public string ExamTitle { get; set; }
        [StringLength(1000)]
        public string ExamDescription { get; set; }
        [StringLength(500)]
        public string ExamPic { get; set; }
        [Column("CompanyID")]
        public int CompanyId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("RoomChatGroupID")]
        public int RoomChatGroupId { get; set; }
        [Column("ExamLoginTypeID")]
        public int ExamLoginTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExamCreateDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExamStartDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExamFinishDateTime { get; set; }
        public int DelayDeadline { get; set; }
        public int ExamTime { get; set; }
        public int OpeningNumber { get; set; }
        public int RandomNumber { get; set; }
        public bool? ExamOn { get; set; }
        public bool? ExamCancel { get; set; }
        [StringLength(500)]
        public string ExamCancelReason { get; set; }
        [Column("ScoreTypeID")]
        public int ScoreTypeId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Exam")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(ExamLoginTypeId))]
        [InverseProperty("Exam")]
        public virtual ExamLoginType ExamLoginType { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("Exam")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
        [ForeignKey(nameof(ScoreTypeId))]
        [InverseProperty("Exam")]
        public virtual ScoreType ScoreType { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Exam")]
        public virtual User User { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<Question> Question { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<Response> Response { get; set; }
    }
}
