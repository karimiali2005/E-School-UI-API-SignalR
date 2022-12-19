using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Homework
    {
        public Homework()
        {
            HomeworkAnswer = new HashSet<HomeworkAnswer>();
        }

        [Key]
        [Column("HomeworkID")]
        public int HomeworkId { get; set; }
        [Column("RoomChatGroupID")]
        public int? RoomChatGroupId { get; set; }
        [Column("RoomID")]
        public int RoomId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Required]
        [StringLength(200)]
        public string HomeworkTile { get; set; }
        [StringLength(200)]
        public string HomeWorkFileName { get; set; }
        public string HomeworkDescription { get; set; }
        [Column("HomeworkTypeID")]
        public int HomeworkTypeId { get; set; }
        [Column("ScoreTypeID")]
        public int? ScoreTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HomeworkStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HomeworkFinishDate { get; set; }
        public bool HomeworkDelete { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Homework")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(HomeworkTypeId))]
        [InverseProperty("Homework")]
        public virtual HomeworkType HomeworkType { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("Homework")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("Homework")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
        [ForeignKey(nameof(ScoreTypeId))]
        [InverseProperty("Homework")]
        public virtual ScoreType ScoreType { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Homework")]
        public virtual User User { get; set; }
        [InverseProperty("Homework")]
        public virtual ICollection<HomeworkAnswer> HomeworkAnswer { get; set; }
    }
}
