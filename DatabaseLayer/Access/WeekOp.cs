using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class WeekOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Week> GetWeeks()
        {
            var query = db.Week;
            return query.AsEnumerable();
        }

        public async Task<string> InsertWeek(Week Week)
        {
            try
            {
                db.Week.Add(Week);
                await db.SaveChangesAsync();
                return Week.WeekId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteWeek(int WeekID)
        {
            try
            {
                var Week = db.Week.Single(p => p.WeekId == WeekID);
                db.Week.Remove(Week);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateWeek(Week Week)
        {
            try
            {
                db.Week.Update(Week);
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
