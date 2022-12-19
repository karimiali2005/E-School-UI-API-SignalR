using Es.DataLayerCore.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ESchool.Api.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateJwtToken(LoginUserResult user)
        {
            

            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.UserID.ToString()));
            claims.Add(new Claim("Nickname", user.FullName));
            //claims.Add(new Claim("IrNational", user.IRNational));

            claims.Add(new Claim("UserTypeId", user.UserTypeID.ToString()));
            claims.Add(new Claim("UserType", user.UserTypeTitle));
            //claims.Add(new Claim("UserActive", Convert.ToString(user.UserActive)));
            //claims.Add(new Claim("ReasonInactive", ReasonInactive));
            //claims.Add(new Claim("MobileNumber", user.MobileNumber));
            //claims.Add(new Claim("PicName", user.PicName ?? ""));
            //claims.Add(new Claim("ExamAddress", user.ExamAddress ?? ""));



            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = Encoding.ASCII.GetBytes("THIS IS For Dr Hesabi School And Mana Pardaz Design");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
