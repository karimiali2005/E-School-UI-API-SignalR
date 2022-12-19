using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DatabaseLayer.Models;
using DatabaseLayer.Need;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoomChatOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public async Task<RoomChatGroup> GetRoomChatGroup(int roomId,int teacherId,int courseId)
        {
            return db.RoomChatGroup.Where(r => r.RoomId == roomId && r.TeacherId == teacherId && r.CourseId == courseId).FirstOrDefault();
        }
        public List<RoomChat> GetRoomChats(int RoomId, int TeacherId, DateTime? dateTime)
        {
            var dtStart = Codes.GetLocalDateTime().AddDays(-15);
            var dtFinish = Codes.GetLocalDateTime().AddDays(1);

            if(dateTime != null)
            {
                dtStart = dateTime.Value;
                dtFinish = dateTime.Value;
            }
            
            var query = db.RoomChat
                .Where(p => p.RoomId == RoomId && p.TeacherId == TeacherId && p.RoomChatDate.Date >= dtStart.Date && p.RoomChatDate.Date <= dtFinish.Date )                
                .AsEnumerable();
            

            return query.ToList();
        }

        public List<RoomChat> GetRoomChats(int TeacherId,int CourseId)
        {
            var dtStart = Codes.GetLocalDateTime().AddDays(-45);
            var dtFinish = Codes.GetLocalDateTime().AddDays(1);

            var query = db.RoomChat
                .Where(p => p.TeacherId == TeacherId && p.RoomChatDate >= dtStart && p.RoomChatDate <= dtFinish&&(p.CourseId==null ||p.CourseId==CourseId))
                .AsEnumerable();


            return query.ToList();
        }        

        public RoomChat GetRoomChat(int RoomChatId)
        {
            try
            {
                var query = db.RoomChat.Where(p => p.RoomChatId == RoomChatId);
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

        public IEnumerable<RoomChat> GetRoomChats(List<int> RoomChatIds)
        {
            try
            {
                var query = db.RoomChat.Where(p => RoomChatIds.Contains(p.RoomChatId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public List<RoomChat> GetRoomChatsByRoomIds(List<int> RoomIds)
        {
            var dtStart = Codes.GetLocalDateTime().AddDays(-15);
            var dtFinish = Codes.GetLocalDateTime().AddDays(1);

            var query = db.RoomChat
                .Where(p => RoomIds.Contains(p.RoomId) && p.RoomChatDate >= dtStart && p.RoomChatDate <= dtFinish)
                .AsEnumerable();


            return query.ToList();
        }

        public async Task<string> InsertRoomChat(int SenderId, string SenderName, int RoomId, int TeacherId,int CourseId,int? GroupId, string TextMessage, string RecieverName, int? RecieverId, bool TagLearn = false, int? RoomChatParentId = null, bool AttachMsg = false, string Filename = "",string MimeType="")
        {
            try
            {
                RoomChat roomChat = new RoomChat()
                {
                    SenderId = SenderId,
                    SenderName = SenderName,
                    RoomChatDate = Codes.GetLocalDateTime(),
                    RoomId = RoomId,
                    TeacherId = TeacherId,
                    CourseId = CourseId,
                    RoomChatGroupId=GroupId,
                    TextChat = TextMessage,
                    Filename = Filename,
                    MimeType= MimeType,
                    RecieverId = RecieverId,
                    RecieverName = RecieverName,
                    RoomChatParentId = RoomChatParentId,
                    TagLearn = TagLearn,
                    AttachMsg = AttachMsg
                };
               
                db.RoomChat.Add(roomChat);
                await db.SaveChangesAsync();
                return roomChat.RoomChatId.ToString();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "-1";
            }
           
        }

        public async Task<string> DeleteRoomChat(int RoomChatID)
        {
            try
            {
                var roomChat = db.RoomChat.Single(p => p.RoomChatId == RoomChatID);
                db.RoomChat.Remove(roomChat);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteRoomChats(List<RoomChat> roomChats)
        {
            try
            {
                db.RoomChat.RemoveRange(roomChats);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateRoomChat(RoomChat roomChat)
        {
            try
            {
                db.RoomChat.Update(roomChat);                
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }


        public async Task<int> UpdateRecord(int RoomChatId, string TextChat)
        {
            try
            {
                var roomChats = db.RoomChat.Where(p => p.RoomChatId == RoomChatId);
                if(roomChats.Count() > 0)
                {
                    var roomChat = roomChats.First();
                    roomChat.TextChat = TextChat;
                    roomChat.RoomChatUpdate = Need.Codes.GetLocalDateTime();
                    db.RoomChat.Update(roomChat);
                    int count = await db.SaveChangesAsync();
                    return count;
                }
                else
                {
                    return 0;
                }                
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return -1;
            }
        }

        public async Task<int> AttachRecord(int RoomChatId, bool AttachMsg)
        {
            try
            {
                var roomChats = db.RoomChat.Where(p => p.RoomChatId == RoomChatId);
                if (roomChats.Count() > 0)
                {
                    var roomChat = roomChats.First();
                    roomChat.AttachMsg = AttachMsg;
                    db.RoomChat.Update(roomChat);
                    int count = await db.SaveChangesAsync();
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return -1;
            }
        }

        public async Task<int> DeleteRecord(int RoomChatId, bool RoomChatDelete = false)
        {
            try
            {
                var roomChats = db.RoomChat.Where(p => p.RoomChatId == RoomChatId);
                if (roomChats.Count() > 0)
                {
                    var roomChat = roomChats.First();
                    roomChat.RoomChatDelete = true;
                    db.RoomChat.Update(roomChat);
                    int count = await db.SaveChangesAsync();
                    return count;
                }
                else
                {
                    return 0;
                }
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
