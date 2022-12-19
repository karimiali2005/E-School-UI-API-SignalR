using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class LogSmsResultOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<LogSmsResult> GetLogSmsResults()
        {
            return db.LogSmsResult.AsEnumerable();
        }

        public string GetLogSmsResultText(int logsmsresult_id)
        {
            try
            {
                var query = db.LogSmsResult.Where(p => p.LogSmsResultId == logsmsresult_id);
                if (query.Count() > 0)
                {
                    return query.First().LogSmsResultText;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "";
            }
        }

        public async Task<string> InsertLogSmsResult(LogSmsResult LogSmsResult)
        {
            try
            {
                db.LogSmsResult.Add(LogSmsResult);
                await db.SaveChangesAsync();
                return LogSmsResult.LogSmsResultId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteLogSmsResult(int LogSmsResultID)
        {
            try
            {
                var LogSmsResult = db.LogSmsResult.Single(p => p.LogSmsResultId == LogSmsResultID);
                db.LogSmsResult.Remove(LogSmsResult);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateLogSmsResult(LogSmsResult LogSmsResult)
        {
            try
            {
                db.LogSmsResult.Update(LogSmsResult);
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
