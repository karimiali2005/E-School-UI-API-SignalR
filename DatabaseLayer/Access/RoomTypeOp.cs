using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class RoomTypeOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();

        public IEnumerable<RoomType> GetRoomTypes()
        {
            var query = db.RoomType;
            return query.AsEnumerable();
        }

        public RoomType GetRoomType(int RoomTypeId)
        {
            var query = db.RoomType.Where(p => p.RoomTypeId == RoomTypeId);
            return query.First();
        }

        public async Task<string> InsertRoomType(RoomType RoomType)
        {
            try
            {
                db.RoomType.Add(RoomType);
                await db.SaveChangesAsync();
                return RoomType.RoomTypeId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteRoomType(int RoomTypeID)
        {
            try
            {
                var RoomType = db.RoomType.Single(p => p.RoomTypeId == RoomTypeID);
                db.RoomType.Remove(RoomType);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> UpdateRoomType(RoomType RoomType)
        {
            try
            {
                db.RoomType.Update(RoomType);
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
