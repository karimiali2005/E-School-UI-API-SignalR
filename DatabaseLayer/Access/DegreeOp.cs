using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class DegreeOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Degree> GetDegrees()
        {
            var query = db.Degree;
            return query.AsEnumerable();
        }

        public async Task<string> InsertDegree(Degree degree)
        {
            try
            {
                db.Degree.Add(degree);
                await db.SaveChangesAsync();
                return degree.DegreeId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteDegree(int DegreeID)
        {
            try
            {
                var degree = db.Degree.Single(p => p.DegreeId == DegreeID);
                db.Degree.Remove(degree);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateDegree(Degree degree)
        {
            try
            {
                db.Degree.Update(degree);
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
