using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using DatabaseLayer.Need;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class CalendarOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public List<Calendar> GetCalendars(string q)
        {
            var query = db.Calendar
                .Select(p => new Calendar
                {
                    CalendarId = p.CalendarId,
                    CalendarDate = p.CalendarDate, 
                    CalendarComment = p.CalendarComment
                })
                .AsEnumerable();

            q = Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
            })) : query;

            return query.OrderByDescending(p => p.CalendarDate).ToList();
        }

        public Calendar GetCalendar(int CalendarId)
        {
            try
            {
                var query = db.Calendar.Where(p => p.CalendarId == CalendarId);
                if (query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<Calendar> GetCalendars(List<int> CalendarIds)
        {
            try
            {
                var query = db.Calendar.Where(p => CalendarIds.Contains(p.CalendarId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<Calendar> GetCalendars(DateTime Start, DateTime End, DayOfWeek? dayOfWeek = null)
        {
            try
            {
                var query = db.Calendar.AsEnumerable();
                query = query.Where(p => p.CalendarDate.Date >= Start.Date && p.CalendarDate.Date <= End.Date);
                if(dayOfWeek != null)
                {
                    query = query.Where(p => p.CalendarDate.DayOfWeek == dayOfWeek);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return new List<Calendar>();
            }
        }

        public bool IsHoliday(DateTime dt)
        {
            var IsHoliday = db.Calendar.Any(p => p.CalendarDate.Date == dt.Date);
            return IsHoliday;
        }

        public async Task<string> InsertCalendar(Calendar calendar)
        {
            try
            {
                db.Calendar.Add(calendar);
                await db.SaveChangesAsync();
                return calendar.CalendarId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteCalendar(int CalendarID)
        {
            try
            {
                var calendar = db.Calendar.Single(p => p.CalendarId == CalendarID);
                db.Calendar.Remove(calendar);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteCalendars(List<Calendar> courses)
        {
            try
            {
                db.Calendar.RemoveRange(courses);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateCalendar(Calendar calendar)
        {
            try
            {
                db.Calendar.Update(calendar);
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
