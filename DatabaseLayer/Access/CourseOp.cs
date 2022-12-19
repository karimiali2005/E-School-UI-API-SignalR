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
    public class CourseOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public List<Course> GetCourses(string q)
        {
            var query = db.Course
                .Select(p => new Course
                {
                    CourseId = p.CourseId,
                    CourseTitle = p.CourseTitle,
                    ReportCardCourseTitle=p.ReportCardCourseTitle
                })
                .AsEnumerable();

            q = Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
            })) : query;

            return query.ToList();
        }

        public Course GetCourse(int CourseId)
        {
            try
            {
                var query = db.Course.Where(p => p.CourseId == CourseId);
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

        public Course GetCourse(string courseTitle,int courseId)
        {
            try
            {
                var query = db.Course.Where(p => p.CourseTitle == courseTitle && p.CourseId!= courseId);
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

        public IEnumerable<Course> GetCourses(List<int> CourseIds)
        {
            try
            {
                var query = db.Course.Where(p => CourseIds.Contains(p.CourseId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public async Task<string> InsertCourse(Course grade)
        {
            try
            {
                db.Course.Add(grade);
                await db.SaveChangesAsync();
                return grade.CourseId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteCourse(int CourseID)
        {
            try
            {
                var grade = db.Course.Single(p => p.CourseId == CourseID);
                db.Course.Remove(grade);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteCourses(List<Course> courses)
        {
            try
            {
                db.Course.RemoveRange(courses);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateCourse(Course grade)
        {
            try
            {
                db.Course.Update(grade);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public bool HasTeacher(int CourseId)
        {
            try
            {
                return db.TeacherCourse.Any(p => p.CourseId == CourseId);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
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
