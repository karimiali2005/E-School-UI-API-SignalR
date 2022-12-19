using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class ParentStatusOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<ParentStatus> GetParentStatuss()
        {
            var query = db.ParentStatus;
            return query.AsEnumerable();
        }

        public async Task<string> InsertParentStatus(ParentStatus parentStatus)
        {
            try
            {
                db.ParentStatus.Add(parentStatus);
                await db.SaveChangesAsync();
                return parentStatus.ParentStatusId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteParentStatus(int ParentStatusID)
        {
            try
            {
                var parentStatus = db.ParentStatus.Single(p => p.ParentStatusId == ParentStatusID);
                db.ParentStatus.Remove(parentStatus);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateParentStatus(ParentStatus parentStatus)
        {
            try
            {
                db.ParentStatus.Update(parentStatus);
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
