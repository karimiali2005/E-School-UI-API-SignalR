using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class LoginPlatformOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<LoginPlatform> GetLoginPlatforms()
        {
            var query = db.LoginPlatform;
            return query.AsEnumerable();
        }

        public async Task<string> InsertLoginPlatform(LoginPlatform LoginPlatform)
        {
            try
            {
                db.LoginPlatform.Add(LoginPlatform);
                await db.SaveChangesAsync();
                return LoginPlatform.LoginPlatformId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteLoginPlatform(int LoginPlatformID)
        {
            try
            {
                var LoginPlatform = db.LoginPlatform.Single(p => p.LoginPlatformId == LoginPlatformID);
                db.LoginPlatform.Remove(LoginPlatform);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateLoginPlatform(LoginPlatform LoginPlatform)
        {
            try
            {
                db.LoginPlatform.Update(LoginPlatform);
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
