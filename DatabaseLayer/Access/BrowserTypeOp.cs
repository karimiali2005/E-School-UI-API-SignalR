using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

//namespace DatabaseLayer.Access
//{
//    public class BrowserTypeOp : IDisposable
//    {
//        ESchoolContext db = new ESchoolContext();

//        public IEnumerable<BrowserType> GetBrowserTypes()
//        {
//            var query = db.BrowserType;

//            return query.AsEnumerable();
//        }

//        public async Task<string> InsertBrowserType(BrowserType BrowserType)
//        {
//            try
//            {
//                db.BrowserType.Add(BrowserType);
//                await db.SaveChangesAsync();
//                return BrowserType.BrowserTypeId.ToString();
//            }
//            catch (DbUpdateException ex)
//            {
//                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
//                return "Error: " + message;
//            }
           
//        }

//        public async Task<string> DeleteBrowserType(int BrowserTypeID)
//        {
//            try
//            {
//                var BrowserType = db.BrowserType.Single(p => p.BrowserTypeId == BrowserTypeID);
//                db.BrowserType.Remove(BrowserType);
//                int count = await db.SaveChangesAsync();
//                return count.ToString();
//            }
//            catch (DbUpdateException ex)
//            {
//                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
//                return "Error: " + message;
//            }
//        }

//        public async Task<string> UpdateBrowserType(BrowserType BrowserType)
//        {
//            try
//            {
//                db.BrowserType.Update(BrowserType);
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
