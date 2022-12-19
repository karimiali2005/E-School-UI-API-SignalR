using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class ObjectOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Models.Object> GetObjects()
        {
            var query = db.Object.Include(s => s.GroupObject);

            return query.AsEnumerable();
        }

        public async Task<string> InsertObject(Models.Object Object)
        {
            try
            {
                db.Object.Add(Object);
                await db.SaveChangesAsync();
                return Object.ObjectId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteObject(int ObjectID)
        {
            try
            {
                var Object = db.Object.Single(p => p.ObjectId == ObjectID);
                db.Object.Remove(Object);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateObject(Models.Object Object)
        {
            try
            {
                db.Object.Update(Object);
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
