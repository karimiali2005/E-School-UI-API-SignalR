using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class LogSmsOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<LogSms> GetLogSmss()
        {
            return db.LogSms.AsEnumerable();
        }

        public async Task<string> InsertLogSms(LogSms LogSms)
        {
            try
            {
                db.LogSms.Add(LogSms);
                await db.SaveChangesAsync();
                return LogSms.LogSmsId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteLogSms(int LogSmsID)
        {
            try
            {
                var LogSms = db.LogSms.Single(p => p.LogSmsId == LogSmsID);
                db.LogSms.Remove(LogSms);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateLogSms(LogSms LogSms)
        {
            try
            {
                db.LogSms.Update(LogSms);
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
