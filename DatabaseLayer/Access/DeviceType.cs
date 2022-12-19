using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

//namespace DatabaseLayer.Access
//{
//    public class DeviceTypeOp : IDisposable
//    {
//        ESchoolContext db = new ESchoolContext();

//        public IEnumerable<DeviceType> GetDeviceTypes()
//        {
//            var query = db.DeviceType;
//            return query.AsEnumerable();
//        }

//        public async Task<string> InsertDeviceType(DeviceType DeviceType)
//        {
//            try
//            {
//                db.DeviceType.Add(DeviceType);
//                await db.SaveChangesAsync();
//                return DeviceType.DeviceTypeId.ToString();
//            }
//            catch (DbUpdateException ex)
//            {
//                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
//                return "Error: " + message;
//            }
//        }

//        public async Task<string> DeleteDeviceType(int DeviceTypeID)
//        {
//            try
//            {
//                var DeviceType = db.DeviceType.Single(p => p.DeviceTypeId == DeviceTypeID);
//                db.DeviceType.Remove(DeviceType);
//                int count = await db.SaveChangesAsync();
//                return count.ToString();
//            }
//            catch (DbUpdateException ex)
//            {
//                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
//                return "Error: " + message;
//            }
//        }

//        public async Task<string> UpdateDeviceType(DeviceType DeviceType)
//        {
//            try
//            {
//                db.DeviceType.Update(DeviceType);
//                int count = await db.SaveChangesAsync();
//                return count.ToString();
//            }
//            catch (DbUpdateException ex)
//            {
//                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
//                return "Error: " + message;
//            }
//        }        

//        private bool disposedValue = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposedValue)
//            {
//                if (disposing)
//                {
//                    db.Dispose();
//                }
//            }
//            this.disposedValue = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}
