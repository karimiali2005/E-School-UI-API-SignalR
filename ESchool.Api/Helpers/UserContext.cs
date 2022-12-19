using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace ESchool.Api.Helpers
{

    public enum ClaimName
    {
        UserId,
        Nickname,
        IrNational,
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
        public static string GetClaimValueString(ClaimName claimType,string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS For Dr Hesabi School And Mana Pardaz Design");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
           


            var claim = jwtToken.Claims.First(x => x.Type == claimType.GetClaimString());
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }
        public static int GetClaimValueInteger(ClaimName claimType, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS For Dr Hesabi School And Mana Pardaz Design");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var claim = jwtToken.Claims.First(x => x.Type == claimType.GetClaimString());
            if (claim != null)
                return Convert.ToInt32(claim.Value);
            return 0;
        }
        #endregion
    }
}

