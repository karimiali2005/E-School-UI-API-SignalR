using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class LoginOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Login> GetLogins()
        {
            var query = db.Login.Include(s => s.User).Include(s => s.LoginPlatform);

            return query.AsEnumerable();
        }

        public async Task<string> InsertLogin(Login Login)
        {
            try
            {
                db.Login.Add(Login);
                await db.SaveChangesAsync();
                return Login.LoginId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteLogin(int LoginID)
        {
            try
            {
                var Login = db.Login.Single(p => p.LoginId == LoginID);
                db.Login.Remove(Login);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateLogin(Login Login)
        {
            try
            {
                db.Login.Update(Login);
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
