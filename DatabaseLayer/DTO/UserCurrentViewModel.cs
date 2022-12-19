using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;

namespace DatabaseLayer.DTO
{
    public class UserCurrentViewModel
    {
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
        public string ReportCardAddress { get; set; }


        public virtual Degree Degree { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ParentStatus ParentStatus { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual User UserIdfatherNavigation { get; set; }
        public virtual User UserIdmotherNavigation { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
