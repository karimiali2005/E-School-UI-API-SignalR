using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EsServiceCore.DTOs.HomeWork;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESchool.Need
{
    public class Codes
    {

        public static string GetResource(string Name)
        {
            try
            {
                string culture = "fa-IR";
                CultureInfo cultureInfo = new CultureInfo(culture);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ESchool.Resources.Messages", System.Reflection.Assembly.GetExecutingAssembly());
                string st = rm.GetString(Name, cultureInfo);
                return st;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return "";
            }
        }

        public static string CreateCode(bool onlyNumber = true, int length = 6)
        {
            string valid = onlyNumber == true ? "1234567890" : "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        public static string GetDomainName(bool withHttp = false)
        {
            var request = System.Web.MyHttpContext.Current.Request;
            string domainName = withHttp ? request.Scheme + "://" + request.Host + request.PathBase : request.Host + request.PathBase;
            return domainName;
        }

        public static string GetDomainPrefixWS()
        {
            var request = System.Web.MyHttpContext.Current.Request;
            string schema = request.Scheme == "http" ? "ws" : "wss";
            return schema;
        }

        public static string getSubr(string t)
        {
            var text = Regex.Replace(t, @"^(.{20}[^\s]*).*", "$1");
            if (t.Length > text.Length)
            {
                text += " ...";
            }
            return text;
        }


        public static DateTime GetLocalDateTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            return localTime;
        }

        public static DateTime? PersianDateToDate(string _year, string _month, string _day)
        {
            try
            {
                string persianDate = _year + "/" + _month + "/" + _day;
                CultureInfo persianCulture = new CultureInfo("fa-IR");
                DateTime persianDateTime = DateTime.ParseExact(persianDate, "yyyy/MM/dd", persianCulture);
                return persianDateTime;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return null;
            }
        }

        public static string getPersianDate()
        {
            DateTime dt = GetLocalDateTime();
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);

            return Year + "/" + Month + "/" + Day;
        }

        public static string getPersianDate(DateTime dt)
        {
            if (dt == null)
                return "";

            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);

            return Year + "/" + Month + "/" + Day;
        }


        public static string getPersianDay(DateTime dt)
        {
            if (dt == null)
                return "";

            PersianCalendar pc = new PersianCalendar();
            DayOfWeek dayOfWeek = pc.GetDayOfWeek(dt);
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return getDay(6);
                case DayOfWeek.Monday:
                    return getDay(2);
                case DayOfWeek.Sunday:
                    return getDay(1);
                case DayOfWeek.Saturday:
                    return getDay(0);
                case DayOfWeek.Wednesday:
                    return getDay(4);
                case DayOfWeek.Thursday:
                    return getDay(5);
                case DayOfWeek.Tuesday:
                    return getDay(3);
            }

            return "";
        }

        public static Dictionary<int, string> getDays()
        {
            Dictionary<int, string> days = new Dictionary<int, string>();
            days.Add(0, "شنبه");
            days.Add(1, "یک شنبه");
            days.Add(2, "دوشنبه");
            days.Add(3, "سه شنبه");
            days.Add(4, "چهارشنبه");
            days.Add(5, "پنج شنبه");
            days.Add(6, "جمعه");

            return days;
        }

        public static string getDay(int index)
        {
            Dictionary<int, string> days = getDays();
            return days[index];
        }

        public static DayOfWeek getDayofWeek(string week_day)
        {
            week_day = string.IsNullOrEmpty(week_day) ? getDay(0) : week_day;

            if (week_day == getDay(0))
                return DayOfWeek.Saturday;
            if (week_day == getDay(1))
                return DayOfWeek.Sunday;
            if (week_day == getDay(2))
                return DayOfWeek.Monday;
            if (week_day == getDay(3))
                return DayOfWeek.Tuesday;
            if (week_day == getDay(4))
                return DayOfWeek.Wednesday;
            if (week_day == getDay(5))
                return DayOfWeek.Thursday;
            if (week_day == getDay(6))
                return DayOfWeek.Friday;

            return DayOfWeek.Saturday;
        }

        public static string getPersianDateTime()
        {
            DateTime dt = GetLocalDateTime();

            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);
            string time = dt.ToString("HHmmss");

            return Year + Month + Day + "_" + time;
        }

        public static byte[] ResizeImage(byte[] pic, int width = 50, int height = 50)
        {
            try
            {
                using (var ms = new MemoryStream(pic))
                {
                    Image img = Image.FromStream(ms);
                    Bitmap bmp = new Bitmap(img, new Size(width, height));
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        string mimeType = GeMimeTypeFromImageByteArray(pic);
                        if (mimeType == "imgae/png")
                        {
                            bmp.Save(ms2, ImageFormat.Png);
                        }
                        else
                        {
                            bmp.Save(ms2, ImageFormat.Jpeg);
                        }
                        return ms2.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return pic;
            }
        }


        public static string GeMimeTypeFromImageByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (Image image = Image.FromStream(stream))
            {
                return ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
            }
        }


        public static string GetPathAndFilename(string filename)
        {
            return AppSettingClass.GetPathStoreFiles() + filename;
        }

        public static string GetFileName(string filename)
        {
            var fullname = AppSettingClass.GetPathStoreFiles() + filename;
            if (System.IO.File.Exists(fullname))
            {
                string extension = System.IO.Path.GetExtension(fullname);
                string newName = Guid.NewGuid().ToString().ToLower().Replace("-", "");
                return newName + extension;
            }
            return filename;
        }
      
        public static string GetRandomFileName(string filename)
        {
            string extension = System.IO.Path.GetExtension(filename);
            string newName = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            return newName + extension;
        }

        public static string GetMimeType(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            filename = GetPathAndFilename(filename);
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out contentType);
            return contentType;
        }

        public static string FileChat(string filename,string addressDownload, bool tagLearn,bool ishomework=false)
        {
            string style = "";
            if (string.IsNullOrEmpty(filename))
                return "";

            if (!ishomework)
            {
                if (tagLearn == true)
                    addressDownload += "Learn/";
                else
                    addressDownload += "Normal/";

            }
            else
            {
                style="style=\" width: 100px; height: 100px;\"";
                addressDownload += "Homework/";
            }
           

            //var fullpath = GetPathAndFilename(filename);
            //string mimetype = MimeMapping.MimeUtility.GetMimeMapping(fullpath); 
            string mimetype = Codes.GetMimeType(filename);
            var file = "<div class=\"file\" >";


            string[] mime_types_images = { "image/jpeg", "image/png", "image/gif" };
            string[] mime_types_audios = { "audio/wav", "audio/mp3", "audio/ogg", "audio/mpeg", "audio/mp4", "audio/aac", "video/ogg" };
            string[] mime_types_videos = { "video/mp4", "video/webm", "video/quicktime" };

            string extension = System.IO.Path.GetExtension(filename);

            if (mimetype == "video/quicktime"&& extension.ToLower()==".mov")
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"video/mp4\"></video>";
            }
            else
            if (mime_types_images.Contains(mimetype))
            {
                file += "<a href=\""+addressDownload + filename + "\">" + "<img src=\"" + addressDownload + filename + "\" " +style+ " />" + "</a>";
            }
            else if (mime_types_audios.Contains(mimetype))
            {
                file += "<audio controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></audio>";
            }
            else if (mime_types_videos.Contains(mimetype))
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></video>";
            }
            else
            {
                file += "<a href=\"" + addressDownload + filename + "\"><img class=\"icon\" src=\"/imagesuser/icons/folder.png\" /><span>" + filename + "</span></a>";
            }

            file += "</div>";
            return file;
        }
        public static string FileChatHomework(string filename, string addressDownload, bool tagLearn, bool ishomework = false)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            if (!ishomework)
            {
                if (tagLearn == true)
                    addressDownload += "Learn/";
                else
                    addressDownload += "Normal/";

            }
            else
            {
               
                addressDownload += "Homework/";
            }

            string style = "style=\" width: 20px; height: 20px;\"";
            //var fullpath = GetPathAndFilename(filename);
            //string mimetype = MimeMapping.MimeUtility.GetMimeMapping(fullpath); 
            string mimetype = Codes.GetMimeType(filename);
            var file = "<div class=\"file\" >";

            string[] mime_types_images = { "image/jpeg", "image/png", "image/gif" };
            string[] mime_types_audios = { "audio/wav", "audio/mp3", "audio/ogg", "audio/mpeg","audio/mp4", "audio/aac", "video/ogg" };
            string[] mime_types_videos = { "video/mp4", "video/webm", "video/quicktime" };
           
            string extension = System.IO.Path.GetExtension(filename);

            if (mimetype == "video/quicktime" && extension.ToLower() == ".mov")
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"video/mp4\"></video>";
            }
            else
            if (mime_types_images.Contains(mimetype))
            {
                file += "<a target='_blank' href=\"" + addressDownload + filename + "\">" + "<img src=\"" + addressDownload + filename + "\"  />" + "</a>";
            }
            else if (mime_types_audios.Contains(mimetype))
            {
                file += "<audio controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></audio>";
            }
            else if (mime_types_videos.Contains(mimetype))
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></video>";
            }
            else
            {
                file += "<a  target='_blank' href=\"" + addressDownload + filename + "\"><img class=\"icon\" src=\"/imagemember/404File.png\" /></a>";
            }
            file += "<a  target='_blank' href=\"" + addressDownload + filename + "\" class=\"dl-file-user btn btn-info d-flex align-items-start\" ><img class=\"icon\" src=\"/imagemember/down-arrow.svg\"  " + style+"/></a>";
            file += "</div>";
            return file;
        }
        public static string FileChatHomeworkStudents(string filename, string addressDownload, bool tagLearn, bool ishomework = false)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            if (!ishomework)
            {
                if (tagLearn == true)
                    addressDownload += "Learn/";
                else
                    addressDownload += "Normal/";

            }
            else
            {

                addressDownload += "Homework/";
            }
            string style = "style=\" width: 20px; height: 20px;\"";

            //var fullpath = GetPathAndFilename(filename);
            //string mimetype = MimeMapping.MimeUtility.GetMimeMapping(fullpath); 
            string mimetype = Codes.GetMimeType(filename);
            var file = "<div class=\"file\" >";

            //string[] mime_types_images = { "image/jpeg", "image/png", "image/gif" };
            //string[] mime_types_audios = { "audio/wav", "audio/mp3", "audio/ogg", "audio/mpeg" };
            //string[] mime_types_videos = { "video/mp4", "video/webm", "video/ogg" };

            //string extension = System.IO.Path.GetExtension(filename);

            //if (mimetype == "video/quicktime" && extension.ToLower() == ".mov")
            //{
            //    file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"video/mp4\"></video>";
            //}
            //else
            //if (mime_types_images.Contains(mimetype))
            //{
            //    file += "<a href=\"" + addressDownload + filename + "\">" + "<img src=\"" + addressDownload + filename + "\"  />" + "</a>";
            //}
            //else if (mime_types_audios.Contains(mimetype))
            //{
            //    file += "<audio controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></audio>";
            //}
            //else if (mime_types_videos.Contains(mimetype))
            //{
            //    file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></video>";
            //}
            //else
            //{
            //    file += "<a href=\"" + addressDownload + filename + "\"><img class=\"icon\" src=\"/imagesuser/icons/folder.png\" /><span>" + filename + "</span></a>";
            //}
            file += "<a  target='_blank' href=\"" + addressDownload + filename + "\" class=\"dl-file-user btn btn-info d-flex align-items-start\" ><img class=\"icon\" src=\"/imagemember/down-arrow.svg\"  " + style + "/></a>";
            file += "</div>";
            return file;
        }
        public static string FileChatExamAndResponse(string filename, string addressDownload,  bool isResponse = false)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            if (isResponse)
            {

                addressDownload += "Response/";
            }
            else
            {
                addressDownload += "Exam/";
            }

            string style = "style=\" width: 20px; height: 20px;\"";
            //var fullpath = GetPathAndFilename(filename);
            //string mimetype = MimeMapping.MimeUtility.GetMimeMapping(fullpath); 
            string mimetype = Codes.GetMimeType(filename);
            var file = "<div class=\"file\" >";

            string[] mime_types_images = { "image/jpeg", "image/png", "image/gif" };
            string[] mime_types_audios = { "audio/wav", "audio/mp3", "audio/ogg", "audio/mpeg", "audio/mp4", "audio/aac", "video/ogg" };
            string[] mime_types_videos = { "video/mp4", "video/webm", "video/quicktime" };

            string extension = System.IO.Path.GetExtension(filename);

            if (mimetype == "video/quicktime" && extension.ToLower() == ".mov")
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"video/mp4\"></video>";
            }
            else
            if (mime_types_images.Contains(mimetype))
            {
                file += "<a target='_blank' href=\"" + addressDownload + filename + "\">" + "<img src=\"" + addressDownload + filename + "\"  />" + "</a>";
            }
            else if (mime_types_audios.Contains(mimetype))
            {
                file += "<audio controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></audio>";
            }
            else if (mime_types_videos.Contains(mimetype))
            {
                file += "<video controls><source src=\"" + addressDownload + filename + "\" type=\"" + mimetype + "\"></video>";
            }
            else
            {
                file += "<a  target='_blank' href=\"" + addressDownload + filename + "\"><img class=\"icon\" src=\"/imagemember/404File.png\" /></a>";
            }
            file += "<a  target='_blank' href=\"" + addressDownload + filename + "\" class=\"dl-file-user btn btn-info d-flex align-items-start\" ><img class=\"icon\" src=\"/imagemember/down-arrow.svg\"  " + style + "/></a>";
            file += "</div>";
            return file;
        }
        public static string GetPathStoreProfileImage(string fileName,bool isThumb=true)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "null")
            {
                return Path.GetFullPath("wwwroot/imagemember/picstucdent.png");
            }
            else
            {
                string path = Codes.GetDomainName().ToLower().Contains("localhost")
                    ? SettingContext.PathStoreProfileImage.Instance.Path1
                    : SettingContext.PathStoreProfileImage.Instance.Path2;
                if (isThumb)
                    return path + "ImageUserThumb\\" + fileName;
                else
                    return path + "ImageUser\\" + fileName;
            }

        }
        //public static string GetPathStoreFishImage(string fileName)
        //{
         
        //        string path = SettingContext.PathStoreProfileImage.Instance.Path3;
                    

        //       return path + fileName;
           
        //}
        public static byte[] filePicDefault = null;
        public static  byte[] ConvertImageToArray(string fn, bool isThumb = true)
        {

            
            if (string.IsNullOrEmpty(fn))
            {
                if (filePicDefault == null)
                {
                    var fullpath = Path.GetFullPath("wwwroot/imagemember/picstucdent.png");
                    FileStream stream = File.OpenRead(fullpath);
                    byte[] fileBytes = new byte[stream.Length];

                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();
                    filePicDefault = fileBytes;
                }
                
                return filePicDefault;
            }
            else
            {
                var fullpath = Codes.GetPathStoreProfileImage(fn, isThumb);
                if (System.IO.File.Exists(fullpath))
                {
                    FileStream stream = File.OpenRead(fullpath);
                    byte[] fileBytes = new byte[stream.Length];

                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();
                    return fileBytes;
                }
                else
                {
                    fullpath = Path.GetFullPath("wwwroot/imagemember/picstucdent.png");
                    FileStream stream = File.OpenRead(fullpath);
                    byte[] fileBytes = new byte[stream.Length];

                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();
                    filePicDefault = fileBytes;
                    return filePicDefault;
                }
            }
            
          
        }

        public static string GetFileNameNew(string fileName)
        {
            var newName = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            var ext = Path.GetExtension(fileName);

            var fileNameNew = newName + ext;
            return fileNameNew;
        }

        public static void ResizeImage(IFormFile file,string fileNameNew, int width = 50, int height = 50)
        {
            try
            {


                var img = Image.FromStream(file.OpenReadStream(), true, true);
                

                Bitmap bmp = new Bitmap(img, new Size(width, height));
                using (MemoryStream ms2 = new MemoryStream())
                {
                    var ext = Path.GetExtension(file.FileName);
                    if (ext.ToLower() == ".png")
                    {
                        bmp.Save(ms2, ImageFormat.Png);
                    }
                    else
                    {
                        bmp.Save(ms2, ImageFormat.Jpeg);
                    }
                    Image.FromStream(ms2).Save(fileNameNew);
                }
                
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();

            }
        }

        public static void ResizeImage(byte[] pic, string fileNameNew, int width = 50, int height = 50)
        {
            try
            {


                using (var ms = new MemoryStream(pic))
                {
                    Image img = Image.FromStream(ms);
                    Bitmap bmp = new Bitmap(img, new Size(width, height));
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        string mimeType = GeMimeTypeFromImageByteArray(pic);
                        if (mimeType == "imgae/png")
                        {
                            bmp.Save(ms2, ImageFormat.Png);
                        }
                        else
                        {
                            bmp.Save(ms2, ImageFormat.Jpeg);
                        }
                        Image.FromStream(ms2).Save(fileNameNew);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();

            }
        }

        public static bool CopyFile(string addressCopy,string addressPaste,string fileName, string FileToCopy, string userName, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(addressCopy + fileName);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    request.Credentials = new NetworkCredential(userName, password);
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    Upload(addressPaste + FileToCopy, ToByteArray(responseStream), userName, password);
                    responseStream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();

            }
            return false;
        }

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public static bool Upload(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                    clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                    clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                    System.IO.Stream clsStream = clsRequest.GetRequestStream();
                    clsStream.Write(Image, 0, Image.Length);

                    clsStream.Close();
                    clsStream.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();

            }
            return false;
            
        }

        public static bool DeleteFile(string addressCopy, string fileName, string userName, string password)
        {
            try
            {  if (!string.IsNullOrEmpty(fileName))
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(addressCopy + fileName);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    request.Credentials = new NetworkCredential(userName, password);
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    response.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();

            }
            return false;
        }
    }
}
