using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoomTeacherOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public List<RoomTeacher> GetRoomTeachers(int UserId)
        {
            var query = db.RoomTeacher.Include(s => s.Course).Include(s => s.Room).Where(p => p.UserId == UserId);
            return query.ToList();
        }

        public IEnumerable<RoomTeacher> GetRoomTeachers(List<int> RoomTeacherIds)
        {
            try
            {
                var query = db.RoomTeacher.Include(s => s.User).Include(s => s.User).Include(s => s.Course).Include(s => s.Room).Where(p => RoomTeacherIds.Contains(p.RoomTeacherId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<RoomTeacher> GetRoomTeachersByRoomId(int RoomId)
        {
            try
            {
                var query = db.RoomTeacher.Where(p => p.RoomId == RoomId);
                return query.ToList();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<RoomTeacher> GetRoomTeachers(int UserId, int RoomId)
        {
            try
            {
                var query = db.RoomTeacher.Where(p => p.UserId == UserId && p.RoomId == RoomId);
                return query.ToList();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<RoomTeacher> GetRoomTeachers(int UserId, int RoomId, int CourseId)
        {
            try
            {
                var query = db.RoomTeacher.Include(s => s.User).Include(s => s.Room).Include(s => s.Course)
                    .Where(p => p.UserId == UserId && p.RoomId == RoomId && p.CourseId == CourseId);
                return query.ToList();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public RoomTeacher GetRoomTeacher(int RoomTeacherId)
        {
            var query = db.RoomTeacher.Include(s => s.User).Include(s => s.Room).Include(s => s.Course).Where(p => p.RoomTeacherId == RoomTeacherId);
            if (query.Count() > 0)
            {
                return query.First();
            }
            else
            {
                return null;
            }
        }

        public bool HasTeacher(int UserId, int RoomId)
        {
            return db.RoomTeacher.Any(p => p.UserId == UserId && p.RoomId == RoomId);
        }

        public bool HasTeacher(int UserId, int RoomId, int CourseId)
        {
            return db.RoomTeacher.Any(p => p.UserId == UserId && p.RoomId == RoomId && p.CourseId == CourseId);
        }

        public async Task<string> InsertRoomTeacher(RoomTeacher RoomTeacher)
        {
            try
            {
                db.RoomTeacher.Add(RoomTeacher);
                await db.SaveChangesAsync();
                return RoomTeacher.RoomTeacherId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteRoomTeacher(int RoomTeacherID)
        {
            try
            {
                var RoomTeacher = db.RoomTeacher.Single(p => p.RoomTeacherId == RoomTeacherID);
                db.RoomTeacher.Remove(RoomTeacher);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteRoomTeachers(List<RoomTeacher> teacherTeachers)
        {
            try
            {
                db.RoomTeacher.RemoveRange(teacherTeachers);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateRoomTeacher(RoomTeacher RoomTeacher)
        {
            try
            {
                db.RoomTeacher.Update(RoomTeacher);
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
