using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoleOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Role> GetRoles()
        {
            var query = db.Role;
            return query.AsEnumerable();
        }

        public async Task<string> InsertRole(Role Role)
        {
            try
            {
                db.Role.Add(Role);
                await db.SaveChangesAsync();
                return Role.RoleId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteRole(int RoleID)
        {
            try
            {
                var Role = db.Role.Single(p => p.RoleId == RoleID);
                db.Role.Remove(Role);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateRole(Role Role)
        {
            try
            {
                db.Role.Update(Role);
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
