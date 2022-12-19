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
    public class RoomOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public List<Room> GetRooms()
        {
            var query = db.Room.Include(s => s.Degree).Include(s => s.Grade).Include(s => s.RoomType).Include(s => s.StudyField).Include(s => s.RoomType)                
                .Select(p => new Room
                {
                    RoomTitle = p.RoomTitle,
                    Degree = p.Degree,
                    DegreeId = p.DegreeId,
                    Grade = p.Grade,
                    GradeId = p.GradeId,
                    StudyField = p.StudyField,
                    StudyFieldId = p.StudyFieldId,
                    //RoomDetail = p.RoomDetail,
                    RoomUser = p.RoomUser,
                    RoomId = p.RoomId,
                    RoomType = p.RoomType,
                    RoomTypeId = p.RoomTypeId,
                    RoomStartDate = p.RoomStartDate,
                    RoomFinishDate = p.RoomFinishDate,
                    //CloseOnTime = p.CloseOnTime,
                    //CloseChat = p.CloseChat,
                    //PermissionCloseChat = p.PermissionCloseChat,
                    //PermissionStudentChatDelete = p.PermissionStudentChatDelete,
                    //PermissionStudentChatEdit = p.PermissionStudentChatEdit,
                    //PinRoomChatId = p.PinRoomChatId,
                    //RoomChat = p.RoomChat
                })
                .AsEnumerable();            

            return query.ToList();
        }

        public List<Room> GetRooms(int degreeId, int gradeId, int studyFieldId, string q, int roomtypeid, DateTime start, DateTime finish)
        {
            var query = db.Room.Include(s => s.Degree).Include(s => s.Grade).Include(s => s.RoomType).Include(s => s.StudyField).Include(s => s.RoomType)
                .Where(s => s.RoomStartDate.CompareTo(start) >= 0 && s.RoomFinishDate.CompareTo(finish) <= 0)
                .Select(p => new Room
                {
                    RoomTitle = p.RoomTitle,
                    Degree = p.Degree,
                    DegreeId = p.DegreeId,
                    Grade = p.Grade,
                    GradeId = p.GradeId,
                    StudyField = p.StudyField,
                    StudyFieldId = p.StudyFieldId,
                    RoomDetail = p.RoomDetail,
                    RoomUser = p.RoomUser,
                    RoomId = p.RoomId,
                    RoomType = p.RoomType,
                    RoomTypeId = p.RoomTypeId,
                    RoomStartDate = p.RoomStartDate,
                    RoomFinishDate = p.RoomFinishDate,
                    CloseOnTime = p.CloseOnTime,
                    CloseChat = p.CloseChat,
                    PermissionCloseChat = p.PermissionCloseChat,
                    PermissionStudentChatDelete = p.PermissionStudentChatDelete,
                    PermissionStudentChatEdit = p.PermissionStudentChatEdit,
                    //PinRoomChatId = p.PinRoomChatId,
                    //RoomChat = p.RoomChat
                })
                .AsEnumerable();
            query = roomtypeid > 0 ? query.Where(p => p.RoomTypeId == roomtypeid) : query;
            query = degreeId > 0 ? query.Where(p => p.DegreeId == degreeId) : query;
            query = gradeId > 0 ? query.Where(p => p.GradeId == gradeId) : query;
            query = studyFieldId > 0 ? query.Where(p => p.StudyFieldId == studyFieldId) : query;

            q = Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
            })) : query;

            return query.ToList();
        }

        public Room GetRoom(int RoomId)
        {
            try
            {
                var query = db.Room.Include(s => s.RoomType).Where(p => p.RoomId == RoomId);
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

        public IEnumerable<Room> GetRooms(int RoomId)
        {
            try
            {
                var query = db.Room.Include(s => s.Degree).Include(s => s.Grade).Include(s => s.RoomType).Include(s => s.StudyField).Include(s => s.RoomType)
                    .Where(p => p.RoomId == RoomId)
                    .Select(p => new Room
                    {
                        RoomTitle = p.RoomTitle,
                        Degree = p.Degree,
                        DegreeId = p.DegreeId,
                        Grade = p.Grade,
                        GradeId = p.GradeId,
                        StudyField = p.StudyField,
                        StudyFieldId = p.StudyFieldId,
                        RoomDetail = p.RoomDetail,
                        RoomUser = p.RoomUser,
                        RoomId = p.RoomId,
                        RoomType = p.RoomType,
                        RoomTypeId = p.RoomTypeId,
                        RoomStartDate = p.RoomStartDate,
                        RoomFinishDate = p.RoomFinishDate,                        
                        CloseOnTime = p.CloseOnTime,
                        CloseChat = p.CloseChat,
                        PermissionCloseChat = p.PermissionCloseChat,
                        PermissionStudentChatDelete = p.PermissionStudentChatDelete,
                        PermissionStudentChatEdit = p.PermissionStudentChatEdit,
                        PinRoomChatId = p.PinRoomChatId,
                        RoomChat = p.RoomChat
                    });
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<Room> GetRooms(List<int> RoomIds)
        {
            try
            {
                var query = db.Room.Include(s => s.Degree).Include(s => s.Grade).Include(s => s.RoomType).Include(s => s.StudyField).Include(s => s.RoomType)
                    .Where(p => RoomIds.Contains(p.RoomId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<RoomChatGroup> GetRoomChatGroup(int id)
        {
            try
            {
                var query = db.RoomChatGroup
                    .Where(p => p.RoomId==id);
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }

        }
        

        public bool HasRoomDetails(int RoomId)
        {
            return db.RoomDetail.Any(s => s.RoomId == RoomId);
        }

        public bool HasRoomUsers(int RoomId)
        {
            return db.RoomUser.Any(s => s.RoomId == RoomId);
        }

        public async Task<string> InsertRoom(Room Room)
        {
            try
            {
                db.Room.Add(Room);
                await db.SaveChangesAsync();
                return Room.RoomId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteRoom(int RoomID)
        {
            try
            {


                var has = HasRoomDetails(RoomID);
                if(has)
                {
                    return "Error: Has RoomDetail";
                }

                var Room = db.Room.Single(p => p.RoomId == RoomID);                
                db.Room.Remove(Room);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteRooms(List<Room> rooms)
        {
            try
            {
                db.Room.RemoveRange(rooms);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<int> DeleteRoomDetails(List<int> ids)
        {
            try
            {
                var query = db.RoomDetail.Where(p => ids.Contains(p.RoomDetailId)).ToList();
                db.RoomDetail.RemoveRange(query);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<int> DeleteRoomUsers(List<int> ids)
        {
            try
            {
                var query = db.RoomUser.Where(p => ids.Contains(p.RoomUserId)).ToList();
                db.RoomUser.RemoveRange(query);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<int> DeleteRoomUsers(int RoomId)
        {
            try
            {
                var query = db.RoomUser.Where(p => p.RoomId == RoomId).ToList();
                db.RoomUser.RemoveRange(query);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<int> UpdateRoom(Room Room)
        {
            try
            {
                db.Room.Update(Room);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return -1;
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
