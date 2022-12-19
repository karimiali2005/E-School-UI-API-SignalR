using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Models
{
    public class UserRegister
    {
        public int Degree { get; set; }
        public int Grade { get; set; }
        public int StudyField { get; set; }


        public string studentName { get; set; }
        public string studentFamily { get; set; }
        public string studentIRNational { get; set; }
        public string studentCertificateSerial { get; set; }
        public string studentCertificateNum { get; set; }
        public string studentCertificateChar { get; set; }
        public string studentLocationBirth { get; set; }
        public string studentLocationCertificate { get; set; }
        public string studentBirthDay { get; set; }
        public string studentBirthMonth { get; set; }
        public string studentBirthYear { get; set; }
        public string studentLivePerson { get; set; }
        public string studentLivePersonOther { get; set; }
        public string studentNumber { get; set; }
        public string studentMobileNumber { get; set; }
        public string studentHomeAddress { get; set; }


        public string fatherName { get; set; }
        public string fatherFamily { get; set; }
        public string fatherIRNational { get; set; }
        public string fatherCertificateSerial { get; set; }
        public string fatherCertificateNum { get; set; }
        public string fatherCertificateChar { get; set; }
        public string fatherLocationBirth { get; set; }
        public string fatherLocationCertificate { get; set; }
        public string fatherBirthDay { get; set; }
        public string fatherBirthMonth { get; set; }
        public string fatherBirthYear { get; set; }
        public string fatherDegree { get; set; }
        public string fatherjob { get; set; }
        public string fatherNumber { get; set; }
        public string fatherMobileNumber { get; set; }
        public string fatherJobAddress { get; set; }
        public int fatherStatus { get; set; }


        public string motherName { get; set; }
        public string motherFamily { get; set; }
        public string motherIRNational { get; set; }
        public string motherCertificateSerial { get; set; }
        public string motherCertificateNum { get; set; }
        public string motherCertificateChar { get; set; }
        public string motherLocationBirth { get; set; }
        public string motherLocationCertificate { get; set; }
        public string motherBirthDay { get; set; }
        public string motherBirthMonth { get; set; }
        public string motherBirthYear { get; set; }
        public string motherDegree { get; set; }
        public string motherjob { get; set; }
        public string motherNumber { get; set; }
        public string motherMobileNumber { get; set; }
        public string motherJobAddress { get; set; }
        public int motherStatus { get; set; }


        public string Nationality { get; set; }
        public string Insurance { get; set; }
        public int FamilyNumber { get; set; }
        public int SeveralChildren { get; set; }

        public bool RightHanded { get; set; }
        public bool PersianLanguage { get; set; }
        public bool IsRelativesParents { get; set; }
        public bool IsStudentInsurance { get; set; }
        public bool PreschoolEducation { get; set; }
        public bool IsCityPlace { get; set; }
        public bool ReferralReasonNew { get; set; }


        public IFormFile file { get; set; }        


    }
}
