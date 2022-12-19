using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class User
    {
        public User()
        {
            InverseUserIdfatherNavigation = new HashSet<User>();
            InverseUserIdmotherNavigation = new HashSet<User>();
            Login = new HashSet<Login>();
            RoomChatReciever = new HashSet<RoomChat>();
            RoomChatSender = new HashSet<RoomChat>();
            RoomChatTeacher = new HashSet<RoomChat>();
            RoomDetail = new HashSet<RoomDetail>();
            RoomTeacher = new HashSet<RoomTeacher>();
            RoomUser = new HashSet<RoomUser>();
            TeacherCourse = new HashSet<TeacherCourse>();
            UserRole = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public int? UserIdfather { get; set; }
        public int? UserIdmother { get; set; }
        public int UserTypeId { get; set; }
        public int? DegreeId { get; set; }
        public int? GradeId { get; set; }
        public int? StudyFieldId { get; set; }
        public string Irnational { get; set; }
        public string Password { get; set; }
        public string PasswordToken { get; set; }
        public string MobileNumber { get; set; }
        public string PicName { get; set; }
        /*public byte[] Pic { get; set; }
        public byte[] PicThumb { get; set; }*/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdcardSerial { get; set; }
        public string IdcardSeriesNumber { get; set; }
        public string IdcardSeriesLetter { get; set; }
        public string IdcardPlace { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string LivePlace { get; set; }
        public string LivePlaceOther { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string ParentDegree { get; set; }
        public string ParentJob { get; set; }
        public int? ParentStatusId { get; set; }
        public bool? RightHanded { get; set; }
        public string Nationality { get; set; }
        public bool? PersianLanguage { get; set; }
        public string Insurance { get; set; }
        public bool? IsStudentInsurance { get; set; }
        public bool? PreschoolEducation { get; set; }
        public bool? IsCityPlace { get; set; }
        public bool? ReferralReasonNew { get; set; }
        public int? FamilyNumber { get; set; }
        public int? SeveralChildren { get; set; }
        public bool? IsRelativesParents { get; set; }
        public int? UserActive { get; set; }
        public string ReasonInactive { get; set; }
        public bool IsPersonnel { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ParentStatus ParentStatus { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual User UserIdfatherNavigation { get; set; }
        public virtual User UserIdmotherNavigation { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<User> InverseUserIdfatherNavigation { get; set; }
        public virtual ICollection<User> InverseUserIdmotherNavigation { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<RoomChat> RoomChatReciever { get; set; }
        public virtual ICollection<RoomChat> RoomChatSender { get; set; }
        public virtual ICollection<RoomChat> RoomChatTeacher { get; set; }
        public virtual ICollection<RoomDetail> RoomDetail { get; set; }
        public virtual ICollection<RoomTeacher> RoomTeacher { get; set; }
        public virtual ICollection<RoomUser> RoomUser { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
