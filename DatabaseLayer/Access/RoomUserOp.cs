using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoomUserOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<RoomUser> GetRoomUsers()
        {
            var query = db.RoomUser;

            return query.AsEnumerable();
        }


        public IEnumerable<RoomUser> GetRoomUsers(int RoomId)
        {
            var query = db.RoomUser.Include(x => x.User).Where(p => p.RoomId == RoomId);
            return query.AsEnumerable();
        }


        public List<RoomUser> GetRoomsUsers(int UserId)
        {
            var query = db.RoomUser.Include(s => s.Room).Include(s => s.Room.RoomType).Include(s => s.User).Include(s => s.User.UserType).Where(p => p.UserId == UserId || p.User.UserIdmother == UserId || p.User.UserIdfather == UserId);
            return query.ToList();
        }   


        public List<RoomUser> GetRoomsTeachers(int TeacherId)
        {
            var query = db.RoomDetail.Include(s => s.Room)/*.Include(s => s.Room.RoomUser).Include(s => s.User).Include(s => s.User.UserType)*/.Where(p => p.UserId == TeacherId);
            return query.Select(p => new RoomUser { RoomId = p.RoomId, Room = p.Room, UserId = p.UserId == null ? 0 : p.UserId.Value }).ToList();
        }


        public List<RoomUser> GetRoomIdsTeachers(int TeacherId)
        {
            var query = db.RoomDetail.Include(s => s.Room)/*.Include(s => s.Room.RoomUser).Include(s => s.User).Include(s => s.User.UserType)*/.Where(p => p.UserId == TeacherId);
            return query.Select(p => new RoomUser { RoomId = p.RoomId, Room = p.Room }).Distinct().ToList();
        }
        public IEnumerable<UserCurrentViewModel> GetRoomsUserCurrent(int roomId)
        { 
            var query =(from p in db.User 
                       join romUser in db.RoomUser on p.UserId equals romUser.UserId
                       where romUser.RoomId==roomId
                        select new UserCurrentViewModel()
                        {
                            Irnational = p.Irnational,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Address = p.Address,
                            BirthDate = p.BirthDate,
                            BirthPlace = p.BirthPlace,
                            Degree = p.Degree,
                            DegreeId = p.DegreeId,
                            FamilyNumber = p.FamilyNumber,
                            Grade = p.Grade,
                            GradeId = p.GradeId,
                            IdcardPlace = p.IdcardPlace,
                            IdcardSerial = p.IdcardSerial,
                            IdcardSeriesLetter = p.IdcardSeriesLetter,
                            IdcardSeriesNumber = p.IdcardSeriesNumber,
                            LivePlace = p.LivePlace,
                            LivePlaceOther = p.LivePlaceOther,
                            MobileNumber = p.MobileNumber,
                            ParentDegree = p.ParentDegree,
                            ParentJob = p.ParentJob,
                            ParentStatus = p.ParentStatus,
                            ParentStatusId = p.ParentStatusId,
                            StudyField = p.StudyField,
                            StudyFieldId = p.StudyFieldId,
                            Telephone = p.Telephone,
                            UserIdfather = p.UserIdfather,
                            UserIdmother = p.UserIdmother,
                            UserId = p.UserId,
                            UserType = p.UserType,
                            UserTypeId = p.UserTypeId,
                            UserActive = p.UserActive,
                            ReportCardAddress = romUser.ReportCardAddress
                        }).AsEnumerable();
            return query.ToList();
        }

        public List<Room> GetRoomsUser(int UserId)
        {
            try
            {
                List<int> query = db.RoomUser.Where(p => p.UserId == UserId).Distinct().Select(p => p.RoomId).ToList();
                if (query.Count() > 0)
                {
                    var query2 = db.Room.Where(p => query.Contains(p.RoomId))
                        .Select(p => new Room
                        {
                            RoomTitle = p.RoomTitle,
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
                    return query2.ToList();
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

        public List<RoomUser> GetRoomsTeachersByUserId(int UserId)
        {
            List<int> RoomIds = db.RoomUser.Where(p => p.UserId == UserId).Select(p => p.RoomId).Distinct().ToList();
            var query = db.RoomDetail.Include(s => s.Room).Include(s => s.User).Where(p => p.UserId != null).Where(p => RoomIds.Contains(p.UserId.Value)).Select(a => new RoomUser { Room = a.Room, RoomId = a.RoomId, User = a.User, UserId = a.UserId.Value  });
            return query.ToList();
        }

        public bool IsUserInRoom(int UserId, int RoomId)
        {
            return db.RoomUser.Any(p => p.UserId == UserId && p.RoomId == RoomId);            
        }

        public async Task<string> InsertRoomUser(RoomUser roomUser)
        {
            try
            {
                db.RoomUser.Add(roomUser);
                await db.SaveChangesAsync();
                return roomUser.RoomUserId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> InsertRoomUsers(List<RoomUser> roomUsers)
        {
            try
            {
                db.RoomUser.AddRange(roomUsers);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }

        }

        public async Task<string> UpdateRoomChatGroup(List<RoomChatGroup> roomChatGroups)
        {
            try
            {
                db.RoomChatGroup.UpdateRange(roomChatGroups);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }

        }
        public async Task<string> DeleteRoomUser(int RoomUserID)
        {
            try
            {
                var grade = db.RoomUser.Single(p => p.RoomUserId == RoomUserID);
                db.RoomUser.Remove(grade);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateRoomUser(RoomUser grade)
        {
            try
            {
                db.RoomUser.Update(grade);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public RoomUser GetRoomUser(int roomId,int userId)
        {
            try
            {
                var query = db.RoomUser.Where(r=>r.RoomId==roomId&&r.UserId==userId).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
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
