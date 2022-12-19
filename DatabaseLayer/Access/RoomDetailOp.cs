using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseLayer.Models;
using DatabaseLayer.Need;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoomDetailOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<RoomDetail> GetRoomDetails(int roomid, string q, DateTime start, DateTime finish, DayOfWeek? dayOfWeek)
        {
            var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room)
                .Where(s => s.RoomId == roomid && s.RoomDetailDate.CompareTo(start) >= 0 && s.RoomDetailDate.CompareTo(finish) <= 0).AsEnumerable();

            if(dayOfWeek != null)
            {
                query = query.Where(s => s.RoomDetailDate.DayOfWeek == dayOfWeek).AsEnumerable();
            }

            q = Need.Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Need.Codes.ReplaceForSearch(value.ToString()).Contains(q);
            })) : query;            

            return query.AsEnumerable();
        }

        public IEnumerable<RoomDetail> GetRoomDetails(List<int> RoomDetailIds)
        {
            try
            {
                var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room).Include(s => s.Room.RoomType)
                    .Where(p => RoomDetailIds.Contains(p.RoomDetailId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<RoomDetail> GetRoomDetails(int RoomId, string StartTime, string FinishTime, List<DateTime> Datetimes)
        {
            try
            {
                var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room).Include(s => s.Room.RoomType)
                    .Where(p => p.RoomId == RoomId && p.RoomDetailTimeStart == StartTime && p.RoomDetailTimeFinish == FinishTime && Datetimes.Contains(p.RoomDetailDate)).ToList();
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<RoomDetail> GetRoomDetails(int RoomId, string CourseTitle)
        {
            try
            {
                var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room).Include(s => s.Room.RoomType)
                    .Where(p => p.RoomId == RoomId && p.Course.CourseTitle == CourseTitle).ToList();
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<RoomDetail> GetRoomDetails(int RoomId, int TeacherId)
        {
            try
            {
                var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room).Include(s => s.Room.RoomType)
                    .Where(p => p.RoomId == RoomId && p.UserId == TeacherId).ToList();
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public RoomDetail GetRoomDetail(int RoomDetailId)
        {
            try
            {
                var query = db.RoomDetail.Include(x => x.Room).Where(p => p.RoomDetailId == RoomDetailId);
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

        public List<Room> GetRoomsTeacher(int UserId)
        {
            try
            {
                List<int> query = db.RoomDetail.Where(p => p.UserId == UserId).Distinct().Select(p => p.RoomId ).ToList();                
                if (query.Count() > 0)
                {
                    var query2 = db.Room.Where(p => query.Contains(p.RoomId))
                        .Select(p => new Room
                        {
                            RoomTitle = p.RoomTitle,                            
                            //RoomDetail = p.RoomDetail,
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
                            //RoomChat = p.RoomChat
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

        public int GetRoomDetailId(int RoomId, DateTime Datetime, string StartTime, string FinishTime, int RoomDetailId = 0)
        {
            try
            {
                if(RoomDetailId > 0)
                {
                    var query = db.RoomDetail.Where(p => p.RoomId == RoomId && p.RoomDetailDate == Datetime && p.RoomDetailTimeStart == StartTime && p.RoomDetailTimeFinish == FinishTime && p.RoomDetailId != RoomDetailId);
                    int r = query.Count() > 0 ? query.First().RoomDetailId : 0;
                    return r;
                }
                else
                {
                    var query = db.RoomDetail.Where(p => p.RoomId == RoomId && p.RoomDetailDate == Datetime && p.RoomDetailTimeStart == StartTime && p.RoomDetailTimeFinish == FinishTime);
                    int r = query.Count() > 0 ? query.First().RoomDetailId : 0;
                    return r;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public int GetRoomDetailId(DateTime Datetime, int UserId, string StartTime, string FinishTime, int RoomDetailId = 0)
        {
            try
            {
                if (RoomDetailId > 0)
                {
                    var query = db.RoomDetail.Where(p => p.RoomDetailDate == Datetime && p.UserId == UserId && p.RoomDetailTimeStart == StartTime && p.RoomDetailTimeFinish == FinishTime && p.RoomDetailId != RoomDetailId);
                    int r = query.Count() > 0 ? query.First().RoomDetailId : 0;
                    return r;
                }
                else
                {
                    var query = db.RoomDetail.Where(p => p.RoomDetailDate == Datetime && p.UserId == UserId && p.RoomDetailTimeStart == StartTime && p.RoomDetailTimeFinish == FinishTime);
                    int r = query.Count() > 0 ? query.First().RoomDetailId : 0;
                    return r;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public List<RoomDetailGrouped> GetRoomDetailGrouped(int RoomId, DateTime? StartDate = null, DateTime? FinishDate = null)
        {
            var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room)
                .Where(p => p.RoomId == RoomId).AsEnumerable();

            if (StartDate != null && FinishDate != null)
            {
                query = query.Where(p => p.RoomDetailDate.Date >= StartDate.Value.Date && p.RoomDetailDate.Date <= FinishDate.Value.Date);
            }

            return query.Select(p => new RoomDetailGrouped
                {
                    DateStr = Need.Codes.getPersianDate(p.RoomDetailDate),
                    TimeStr = p.RoomDetailTimeStart + " تا " + p.RoomDetailTimeFinish,
                    DayStr = Need.Codes.getPersianDay(p.RoomDetailDate),
                    CourseName = p.Course == null ? "" : p.Course.CourseTitle,
                    TeacherName = p.User == null ? "" : p.User.FirstName + " " + p.User.LastName,
                    TeacherId = p.User == null ? 0 : p.UserId.Value,
                    CourseId = p.CourseId == null ? 0 : Convert.ToInt32(p.CourseId),
                    Comment = p.Comment,
                    RoomId = p.RoomId,
                    RoomTypeId = p.Room.RoomTypeId,
                }).ToList();                        
        }


        public List<RoomDetailGrouped> GetRoomDetailGroupedTeacher(int TeacherId, DateTime? StartDate = null, DateTime? FinishDate = null)
        {
            var query = db.RoomDetail.Include(s => s.Course).Include(s => s.User).Include(s => s.Room)
                .Where(p => p.UserId == TeacherId /*&& p.RoomDetailDate.Date >= Need.Codes.GetLocalDateTime().Date*/).AsEnumerable();

            if(StartDate != null && FinishDate != null)
            {
                query = query.Where(p => p.RoomDetailDate.Date >= StartDate.Value.Date && p.RoomDetailDate.Date <= FinishDate.Value.Date);
            }

            return query.Select(p => new RoomDetailGrouped
            {
                DateStr = Need.Codes.getPersianDate(p.RoomDetailDate),
                TimeStr = p.RoomDetailTimeStart + " تا " + p.RoomDetailTimeFinish,
                DayStr = Need.Codes.getPersianDay(p.RoomDetailDate),
                CourseName = p.Course == null ? "" : p.Course.CourseTitle,
                TeacherName = p.User == null ? "" : p.User.FirstName + " " + p.User.LastName,
                TeacherId = p.User == null ? 0 : p.UserId.Value,
                CourseId = p.CourseId == null ? 0 : Convert.ToInt32(p.CourseId),
                Comment = p.Comment,
                RoomId = p.RoomId,
                RoomTypeId = p.Room.RoomTypeId,
            }).ToList();
            
        }

        public bool RoomDetailHasTime(int RoomId, string Day, string StartTime, string FinishTime)
        {
            DayOfWeek dayOfWeek = Codes.getDayofWeek(Day);
            DateTime dtToday = Need.Codes.GetLocalDateTime();
            return db.RoomDetail.Any(x => 
            x.RoomDetailDate.Date == dtToday.Date 
            && x.RoomId == RoomId 
            && x.RoomDetailTimeStart.CompareTo(StartTime) >= 0 
            && x.RoomDetailTimeFinish.CompareTo(FinishTime) <= 0);
        }

        public async Task<string> InsertRoomDetail(RoomDetail RoomDetail)
        {
            try
            {
                db.RoomDetail.Add(RoomDetail);
                await db.SaveChangesAsync();
                return RoomDetail.RoomDetailId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
           
        }

        public async Task<string> DeleteRoomDetail(int RoomDetailID)
        {
            try
            {
                var RoomDetail = db.RoomDetail.Single(p => p.RoomDetailId == RoomDetailID);
                db.RoomDetail.Remove(RoomDetail);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }


        public async Task<int> DeleteRoomDetails(List<RoomDetail> roomDetails)
        {
            try
            {
                db.RoomDetail.RemoveRange(roomDetails);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateRoomDetail(RoomDetail RoomDetail)
        {
            try
            {
                db.RoomDetail.Update(RoomDetail);
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
