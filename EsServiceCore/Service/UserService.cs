using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.User;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using EsServiceCore.Utility;
using Microsoft.EntityFrameworkCore;

namespace EsServiceCore.Service
{
    public class UserService:IUserService
    {
        private readonly ESchoolContext _context;
        public UserService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<LoginUserResult> LoginUser(LoginViewModel login)
        {
            var hashPassword = Codes.Hash(login.UsersPass);
            var user=await _context.LoginUserAsync(login.UsersName, hashPassword);
            return user.SingleOrDefault();
        }

        public async Task<LoginUserResult> LoginUserTicket(string userName,string hashPassword)
        {
          
            var user = await _context.LoginUserAsync(userName, hashPassword);
            return user.SingleOrDefault();
        }

        public async Task<bool> UserPlatformAdd(int userId,string platfornName,string mobileName, string tokenFireBase)
        {
            try
            {
                _context.UserFireBaseUpdate(userId, mobileName, platfornName, tokenFireBase);
               
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<User> GetGetById(int userId)
        {
            
            var user = await _context.User.FindAsync(userId);
            return user;
        }

        public async Task<bool> LoginInsert(Login login,string ticket)
        {
            try
            {

                var session = await _context.Session.FirstOrDefaultAsync(s => s.Ticket == ticket);

                if (session != null)
                {
                    login.SessionId = session.SessionId;
                    login.CookieName = session.CookieName;
                    login.SessionSatrtDate = session.SessionSatrtDate;
                }

                await _context.Login.AddAsync(login);
               
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> LoginEnd(int userId)
        {
            try
            {

                 _context.LoginEnd(userId);
                

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<User> TicketApprove(string ticket)
        {
            try
            {

                var user = await _context.User.FirstOrDefaultAsync(s => s.UserTicket == ticket);
                if (user != null && user.UserTicketDateTime >= DateTime.Now)
                {
                    return user;
                }
                else
                    return null;

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> UserSetTicket(int userId)
        {
            try
            {

                var user = await _context.User.FindAsync(userId);
                if (user != null )
                {
                    user.UserTicket = Guid.NewGuid().ToString().Replace("-", "");
                    user.UserTicketDateTime = DateTime.Now.AddMinutes(2);
                    await _context.SaveChangesAsync();
                    return user.UserTicket;
                }
                else
                    return null;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
