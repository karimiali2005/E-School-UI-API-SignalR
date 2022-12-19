using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Room
    {
        public Room()
        {
            Homework = new HashSet<Homework>();
            RoomChat = new HashSet<RoomChat>();
            RoomChatGroup = new HashSet<RoomChatGroup>();
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            RoomUser = new HashSet<RoomUser>();
        }

        [Key]
        [Column("RoomID")]
        public int RoomId { get; set; }
        [Column("RoomTypeID")]
        public int RoomTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoomTitle { get; set; }
        [Column("DegreeID")]
        public int? DegreeId { get; set; }
        [Column("GradeID")]
        public int? GradeId { get; set; }
        [Column("StudyFieldID")]
        public int? StudyFieldId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RoomStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RoomFinishDate { get; set; }
        [Required]
        public bool? CloseOnTime { get; set; }
        public bool CloseChat { get; set; }
        public bool PermissionCloseChat { get; set; }
        public bool PermissionStudentChatEdit { get; set; }
        public bool PermissionStudentChatDelete { get; set; }
        [Column("PinRoomChatID")]
        public int PinRoomChatId { get; set; }

        [ForeignKey(nameof(DegreeId))]
        [InverseProperty("Room")]
        public virtual Degree Degree { get; set; }
        [ForeignKey(nameof(GradeId))]
        [InverseProperty("Room")]
        public virtual Grade Grade { get; set; }
        [ForeignKey(nameof(RoomTypeId))]
        [InverseProperty("Room")]
        public virtual RoomType RoomType { get; set; }
        [ForeignKey(nameof(StudyFieldId))]
        [InverseProperty("Room")]
        public virtual StudyField StudyField { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<Homework> Homework { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<RoomChat> RoomChat { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<RoomChatGroup> RoomChatGroup { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<RoomUser> RoomUser { get; set; }
    }
}
