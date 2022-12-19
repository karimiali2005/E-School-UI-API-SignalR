using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.DTOs.User
{
    public partial class LoginUserResult
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string IRNational { get; set; }
        public DateTime BirthDate { get; set; }
        public int UserTypeID { get; set; }
        public string UserTypeTitle { get; set; }
        public int? UserActive { get; set; }
        public string ReasonInactive { get; set; }
        public string MobileNumber { get; set; }
        public string PicName { get; set; }
        public string Password { get; set; }
        public string ExamAddress { get; set; }
    }
    public partial class LoginUserResult
    {
        [NotMapped]
        public string Token { get; set; }


    }
}
