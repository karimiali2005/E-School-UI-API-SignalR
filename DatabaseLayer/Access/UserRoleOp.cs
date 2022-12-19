using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class UserRoleOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<UserRole> GetUserRoles()
        {
            var query = db.UserRole.Include(s => s.Role).Include(s => s.User);

            return query.AsEnumerable();
        }

        public async Task<string> InsertUserRole(UserRole UserRole)
        {
            try
            {
                db.UserRole.Add(UserRole);
                await db.SaveChangesAsync();
                return UserRole.UserRoleId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteUserRole(int UserRoleID)
        {
            try
            {
                var UserRole = db.UserRole.Single(p => p.UserRoleId == UserRoleID);
                db.UserRole.Remove(UserRole);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateUserRole(UserRole UserRole)
        {
            try
            {
                db.UserRole.Update(UserRole);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }        

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
