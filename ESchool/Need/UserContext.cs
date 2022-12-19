using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace ESchool.Need
{
    public enum ClaimName
    {
        UserId,
        Nickname ,
        IrNational ,
        BirthDate,
        UserTypeId,
        UserType,
        UserActive,
        ReasonInactive,
        MobileNumber,
        PicName,
        ExamAddress
    }
    public static class UserContext
    {
        #region ~( Method )~
        private static string GetClaimString(this ClaimName claimName)
        {
            string myString = null;
            switch (claimName)
            {
                case ClaimName.UserId:
                    myString = "UserId";
                    break;
                case ClaimName.Nickname:
                    myString = "Nickname";
                    break;
                case ClaimName.IrNational:
                    myString = "IrNational";
                    break;
                case ClaimName.BirthDate:
                    myString = "BirthDate";
                    break;
                case ClaimName.UserTypeId:
                    myString = "UserTypeId";
                    break;
                case ClaimName.UserType:
                    myString = "UserType";
                    break;
                case ClaimName.UserActive:
                    myString = "UserActive";
                    break;
                case ClaimName.ReasonInactive:
                    myString = "ReasonInactive";
                    break;
                case ClaimName.MobileNumber:
                    myString = "MobileNumber";
                    break;
                case ClaimName.PicName:
                    myString = "PicName";
                    break;
                case ClaimName.ExamAddress:
                    myString = "ExamAddress";
                    break;
            }

            return myString;
        }
        public static string GetClaimValueString(ClaimName claimType)
        {
            var claim = MyHttpContext.Current.User.FindFirst(claimType.GetClaimString());
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }
        public static int GetClaimValueInteger(ClaimName claimType)
        {
            var claim = MyHttpContext.Current.User.FindFirst(claimType.GetClaimString());
            if (claim != null)
                return Convert.ToInt32(claim.Value);
            return 0;
        }
        #endregion
    }
}
