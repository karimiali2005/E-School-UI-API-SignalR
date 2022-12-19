using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomChat
    {
        public RoomChat()
        {
            Banner = new HashSet<Banner>();
            Ceremony = new HashSet<Ceremony>();
            DisciplineRoomChat = new HashSet<Discipline>();
            DisciplineRoomChatId2Navigation = new HashSet<Discipline>();
            FinancialRoomChat = new HashSet<Financial>();
            FinancialRoomChatId2Navigation = new HashSet<Financial>();
            InverseRoomChatParent = new HashSet<RoomChat>();
            RoomChatView = new HashSet<RoomChatView>();
        }

        [Key]
        [Column("RoomChatID")]
        public int RoomChatId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RoomChatDate { get; set; }
        [Column("SenderID")]
        public int SenderId { get; set; }
        [Required]
        [StringLength(100)]
        public string SenderName { get; set; }
        [Column("RecieverID")]
        public int? RecieverId { get; set; }
        [Required]
        [StringLength(100)]
        public string RecieverName { get; set; }
        [Column("RoomID")]
        public int? RoomId { get; set; }
        [Column("TeacherID")]
        public int? TeacherId { get; set; }
        [Column("CourseID")]
        public int? CourseId { get; set; }
        [Required]
        public string TextChat { get; set; }
        [StringLength(500)]
        public string Filename { get; set; }
        [StringLength(50)]
        public string MimeType { get; set; }
        public bool TagLearn { get; set; }
        public bool RoomChatDelete { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RoomChatUpdate { get; set; }
        [Column("RoomChatParentID")]
        public int? RoomChatParentId { get; set; }
        public bool AttachMsg { get; set; }
        [Column("RoomChatGroupID")]
        public int? RoomChatGroupId { get; set; }
        public int RoomChatViewNumber { get; set; }
        [StringLength(13)]
        public string RoomChatFolder { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("RoomChat")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(RecieverId))]
        [InverseProperty(nameof(User.RoomChatReciever))]
        public virtual User Reciever { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("RoomChat")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(RoomChatGroupId))]
        [InverseProperty("RoomChat")]
        public virtual RoomChatGroup RoomChatGroup { get; set; }
        [ForeignKey(nameof(RoomChatParentId))]
        [InverseProperty(nameof(RoomChat.InverseRoomChatParent))]
        public virtual RoomChat RoomChatParent { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.RoomChatSender))]
        public virtual User Sender { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty(nameof(User.RoomChatTeacher))]
        public virtual User Teacher { get; set; }
        [InverseProperty("RoomChat")]
        public virtual ICollection<Banner> Banner { get; set; }
        [InverseProperty("RoomChat")]
        public virtual ICollection<Ceremony> Ceremony { get; set; }
        [InverseProperty(nameof(Discipline.RoomChat))]
        public virtual ICollection<Discipline> DisciplineRoomChat { get; set; }
        [InverseProperty(nameof(Discipline.RoomChatId2Navigation))]
        public virtual ICollection<Discipline> DisciplineRoomChatId2Navigation { get; set; }
        [InverseProperty(nameof(Financial.RoomChat))]
        public virtual ICollection<Financial> FinancialRoomChat { get; set; }
        [InverseProperty(nameof(Financial.RoomChatId2Navigation))]
        public virtual ICollection<Financial> FinancialRoomChatId2Navigation { get; set; }
        [InverseProperty(nameof(RoomChat.RoomChatParent))]
        public virtual ICollection<RoomChat> InverseRoomChatParent { get; set; }
        [InverseProperty("RoomChat")]
        public virtual ICollection<RoomChatView> RoomChatView { get; set; }
    }
}
