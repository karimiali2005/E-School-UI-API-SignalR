using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class AccessoriesOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Accessories> GetAccessoriess()
        {
            var query = db.Accessories.Include(s => s.Object);

            return query.AsEnumerable();
        }

        public async Task<string> InsertAccessories(Accessories Accessories)
        {
            try
            {
                db.Accessories.Add(Accessories);
                await db.SaveChangesAsync();
                return Accessories.AccessoriesId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteAccessories(int AccessoriesID)
        {
            try
            {
                var Accessories = db.Accessories.Single(p => p.AccessoriesId == AccessoriesID);
                db.Accessories.Remove(Accessories);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateAccessories(Accessories Accessories)
        {
            try
            {
                db.Accessories.Update(Accessories);
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
