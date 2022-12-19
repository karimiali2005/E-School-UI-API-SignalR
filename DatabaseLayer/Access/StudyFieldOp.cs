using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class StudyFieldOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<StudyField> GetStudyFields()
        {
            var query = db.StudyField;
            return query.AsEnumerable();
        }

        public async Task<string> InsertStudyField(StudyField studyField)
        {
            try
            {
                db.StudyField.Add(studyField);
                await db.SaveChangesAsync();
                return studyField.StudyFieldId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteStudyField(int StudyFieldID)
        {
            try
            {
                var studyField = db.StudyField.Single(p => p.StudyFieldId == StudyFieldID);
                db.StudyField.Remove(studyField);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateStudyField(StudyField studyField)
        {
            try
            {
                db.StudyField.Update(studyField);
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
