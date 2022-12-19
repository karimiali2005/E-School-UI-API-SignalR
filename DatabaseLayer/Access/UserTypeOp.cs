using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class UserTypeOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<UserType> GetUserTypes()
        {
            var query = db.UserType;
            return query.AsEnumerable();
        }

        public UserType GetUserTypeWithTitle(string UserTypeTitle)
        {
            return db.UserType.Single(p => p.UserTypeTitle == UserTypeTitle);
        }

        public string GetUserTypeTitleWithId(int id)
        {
            return db.UserType.Single(p => p.UserTypeId == id).UserTypeTitle;
        }

        public async Task<string> InsertUserType(UserType userType)
        {
            try
            {
                db.UserType.Add(userType);
                await db.SaveChangesAsync();
                return userType.UserTypeId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteUserType(int UserTypeID)
        {
            try
            {
                var userType = db.UserType.Single(p => p.UserTypeId == UserTypeID);
                db.UserType.Remove(userType);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateUserType(UserType userType)
        {
            try
            {
                db.UserType.Update(userType);
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
