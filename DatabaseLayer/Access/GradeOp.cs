using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class GradeOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<Grade> GetGrades()
        {
            var query = db.Grade.Include(s => s.Degree);

            return query.AsEnumerable();
        }

        public async Task<string> InsertGrade(Grade grade)
        {
            try
            {
                db.Grade.Add(grade);
                await db.SaveChangesAsync();
                return grade.GradeId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteGrade(int GradeID)
        {
            try
            {
                var grade = db.Grade.Single(p => p.GradeId == GradeID);
                db.Grade.Remove(grade);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateGrade(Grade grade)
        {
            try
            {
                db.Grade.Update(grade);
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
