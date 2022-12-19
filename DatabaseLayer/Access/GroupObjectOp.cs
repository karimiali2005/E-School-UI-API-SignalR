using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class GroupObjectOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<GroupObject> GetGroupObjects()
        {
            var query = db.GroupObject;
            return query.AsEnumerable();
        }

        public async Task<string> InsertGroupObject(GroupObject GroupObject)
        {
            try
            {
                db.GroupObject.Add(GroupObject);
                await db.SaveChangesAsync();
                return GroupObject.GroupObjectId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteGroupObject(int GroupObjectID)
        {
            try
            {
                var GroupObject = db.GroupObject.Single(p => p.GroupObjectId == GroupObjectID);
                db.GroupObject.Remove(GroupObject);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateGroupObject(GroupObject GroupObject)
        {
            try
            {
                db.GroupObject.Update(GroupObject);
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
