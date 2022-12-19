using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoleAccessOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<RoleAccess> GetRoleAccesss()
        {
            var query = db.RoleAccess.Include(s => s.Accessories).Include(s => s.Role);

            return query.AsEnumerable();
        }

        public async Task<string> InsertRoleAccess(RoleAccess RoleAccess)
        {
            try
            {
                db.RoleAccess.Add(RoleAccess);
                await db.SaveChangesAsync();
                return RoleAccess.RoleAccessId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteRoleAccess(int RoleAccessID)
        {
            try
            {
                var RoleAccess = db.RoleAccess.Single(p => p.RoleAccessId == RoleAccessID);
                db.RoleAccess.Remove(RoleAccess);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateRoleAccess(RoleAccess RoleAccess)
        {
            try
            {
                db.RoleAccess.Update(RoleAccess);
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
