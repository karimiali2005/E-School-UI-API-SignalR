using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class TeacherCourseOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public List<TeacherCourse> GetTeacherCourses(int UserId)
        {
            var query = db.TeacherCourse.Include(s => s.Course).Include(s => s.User).Include(s => s.Degree).Include(s => s.Grade).Include(s => s.StudyField).Where(p => p.UserId == UserId);
            return query.ToList();
        }

        public IEnumerable<TeacherCourse> GetTeacherCourses(List<int> TeacherCourseIds)
        {
            try
            {
                var query = db.TeacherCourse.Include(s => s.Course).Include(s => s.User).Include(s => s.Degree).Include(s => s.Grade).Include(s => s.StudyField).Where(p => TeacherCourseIds.Contains(p.TeacherCourseId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<TeacherCourse> GetTeacherCourses(int? DegreeId, int? GradeId, int? StudyFieldId)
        {
            try
            {
                var query = db.TeacherCourse.Include(s => s.Course).Include(s => s.User).Include(s => s.Degree).Include(s => s.Grade).Include(s => s.StudyField)
                    .Where(p => p.DegreeId == DegreeId && p.GradeId == GradeId && p.StudyFieldId == StudyFieldId);
                return query.ToList();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public TeacherCourse GetTeacherCourse(int TeacherCourseId)
        {
            var query = db.TeacherCourse.Include(s => s.Course).Include(s => s.User).Include(s => s.Degree).Include(s => s.Grade).Include(s => s.StudyField).Where(p => p.TeacherCourseId == TeacherCourseId);
            if (query.Count() > 0)
            {
                return query.First();
            }
            else
            {
                return null;
            }
        }

        public bool HasCourse(int UserId, int CourseId, int? DegreeId, int? GradeId, int? StudyFieldId)
        {
            return db.TeacherCourse.Any(p => p.UserId == UserId && p.CourseId == CourseId && p.DegreeId == DegreeId && p.GradeId == GradeId && p.StudyFieldId == StudyFieldId);
            
        }

        public async Task<string> InsertTeacherCourse(TeacherCourse TeacherCourse)
        {
            try
            {
                db.TeacherCourse.Add(TeacherCourse);
                await db.SaveChangesAsync();
                return TeacherCourse.TeacherCourseId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteTeacherCourse(int TeacherCourseID)
        {
            try
            {
                var TeacherCourse = db.TeacherCourse.Single(p => p.TeacherCourseId == TeacherCourseID);
                db.TeacherCourse.Remove(TeacherCourse);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteTeacherCourses(List<TeacherCourse> teacherCourses)
        {
            try
            {
                db.TeacherCourse.RemoveRange(teacherCourses);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateTeacherCourse(TeacherCourse TeacherCourse)
        {
            try
            {
                db.TeacherCourse.Update(TeacherCourse);
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
