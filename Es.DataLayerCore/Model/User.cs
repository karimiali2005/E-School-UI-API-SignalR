using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class User
    {
        public User()
        {
            Discipline = new HashSet<Discipline>();
            Exam = new HashSet<Exam>();
            Financial = new HashSet<Financial>();
            Homework = new HashSet<Homework>();
            HomeworkAnswer = new HashSet<HomeworkAnswer>();
            InverseUserIdfatherNavigation = new HashSet<User>();
            InverseUserIdmotherNavigation = new HashSet<User>();
            Login = new HashSet<Login>();
            ReportCardDetailTeacher = new HashSet<ReportCardDetail>();
            ReportCardDetailUser = new HashSet<ReportCardDetail>();
            ReportCardPaper = new HashSet<ReportCardPaper>();
            Response = new HashSet<Response>();
            RoomChatGroupStudent = new HashSet<RoomChatGroup>();
            RoomChatGroupTeacher = new HashSet<RoomChatGroup>();
            RoomChatReciever = new HashSet<RoomChat>();
            RoomChatSender = new HashSet<RoomChat>();
            RoomChatTeacher = new HashSet<RoomChat>();
            RoomChatView = new HashSet<RoomChatView>();
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            RoomUser = new HashSet<RoomUser>();
            TeacherCourse = new HashSet<TeacherCourse>();
            UserPlatform = new HashSet<UserPlatform>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("UserIDFather")]
        public int? UserIdfather { get; set; }
        [Column("UserIDMother")]
        public int? UserIdmother { get; set; }
        [Column("UserTypeID")]
        public int UserTypeId { get; set; }
        [Column("DegreeID")]
        public int? DegreeId { get; set; }
        [Column("GradeID")]
        public int? GradeId { get; set; }
        [Column("StudyFieldID")]
        public int? StudyFieldId { get; set; }
        [Required]
        [Column("IRNational")]
        [StringLength(10)]
        public string Irnational { get; set; }
        [Required]
        [StringLength(500)]
        public string Password { get; set; }
        [StringLength(11)]
        public string PasswordToken { get; set; }
        [Required]
        [StringLength(11)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string PicName { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Column("IDCardSerial")]
        [StringLength(10)]
        public string IdcardSerial { get; set; }
        [Required]
        [Column("IDCardSeriesNumber")]
        [StringLength(10)]
        public string IdcardSeriesNumber { get; set; }
        [Required]
        [Column("IDCardSeriesLetter")]
        [StringLength(10)]
        public string IdcardSeriesLetter { get; set; }
        [Required]
        [Column("IDCardPlace")]
        [StringLength(50)]
        public string IdcardPlace { get; set; }
        [Required]
        [StringLength(50)]
        public string BirthPlace { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        [StringLength(50)]
        public string LivePlace { get; set; }
        [StringLength(50)]
        public string LivePlaceOther { get; set; }
        [StringLength(10)]
        public string Telephone { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(50)]
        public string ParentDegree { get; set; }
        [StringLength(500)]
        public string ParentJob { get; set; }
        [Column("ParentStatusID")]
        public int? ParentStatusId { get; set; }
        public bool? RightHanded { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        public bool? PersianLanguage { get; set; }
        [StringLength(50)]
        public string Insurance { get; set; }
        public bool? IsStudentInsurance { get; set; }
        public bool? PreschoolEducation { get; set; }
        public bool? IsCityPlace { get; set; }
        public bool? ReferralReasonNew { get; set; }
        public int? FamilyNumber { get; set; }
        public int? SeveralChildren { get; set; }
        public bool? IsRelativesParents { get; set; }
        public int? UserActive { get; set; }
        [StringLength(200)]
        public string ReasonInactive { get; set; }
        public bool IsPersonnel { get; set; }
        [StringLength(50)]
        public string UserTicket { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UserTicketDateTime { get; set; }

        [ForeignKey(nameof(DegreeId))]
        [InverseProperty("User")]
        public virtual Degree Degree { get; set; }
        [ForeignKey(nameof(GradeId))]
        [InverseProperty("User")]
        public virtual Grade Grade { get; set; }
        [ForeignKey(nameof(ParentStatusId))]
        [InverseProperty("User")]
        public virtual ParentStatus ParentStatus { get; set; }
        [ForeignKey(nameof(StudyFieldId))]
        [InverseProperty("User")]
        public virtual StudyField StudyField { get; set; }
        [ForeignKey(nameof(UserIdfather))]
        [InverseProperty(nameof(User.InverseUserIdfatherNavigation))]
        public virtual User UserIdfatherNavigation { get; set; }
        [ForeignKey(nameof(UserIdmother))]
        [InverseProperty(nameof(User.InverseUserIdmotherNavigation))]
        public virtual User UserIdmotherNavigation { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("User")]
        public virtual UserType UserType { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Discipline> Discipline { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Exam> Exam { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Financial> Financial { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Homework> Homework { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<HomeworkAnswer> HomeworkAnswer { get; set; }
        [InverseProperty(nameof(User.UserIdfatherNavigation))]
        public virtual ICollection<User> InverseUserIdfatherNavigation { get; set; }
        [InverseProperty(nameof(User.UserIdmotherNavigation))]
        public virtual ICollection<User> InverseUserIdmotherNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Login> Login { get; set; }
        [InverseProperty(nameof(ReportCardDetail.Teacher))]
        public virtual ICollection<ReportCardDetail> ReportCardDetailTeacher { get; set; }
        [InverseProperty(nameof(ReportCardDetail.User))]
        public virtual ICollection<ReportCardDetail> ReportCardDetailUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<ReportCardPaper> ReportCardPaper { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Response> Response { get; set; }
        [InverseProperty(nameof(RoomChatGroup.Student))]
        public virtual ICollection<RoomChatGroup> RoomChatGroupStudent { get; set; }
        [InverseProperty(nameof(RoomChatGroup.Teacher))]
        public virtual ICollection<RoomChatGroup> RoomChatGroupTeacher { get; set; }
        [InverseProperty(nameof(RoomChat.Reciever))]
        public virtual ICollection<RoomChat> RoomChatReciever { get; set; }
        [InverseProperty(nameof(RoomChat.Sender))]
        public virtual ICollection<RoomChat> RoomChatSender { get; set; }
        [InverseProperty(nameof(RoomChat.Teacher))]
        public virtual ICollection<RoomChat> RoomChatTeacher { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<RoomChatView> RoomChatView { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<RoomUser> RoomUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserPlatform> UserPlatform { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
