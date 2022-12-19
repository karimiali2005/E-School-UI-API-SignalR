using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using APISms.Need;
using DatabaseLayer.Access;
using DatabaseLayer.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using ESchool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenXmlPowerTools;
using Codes = ESchool.Need.Codes;

namespace ESchool.Areas.Panel.Controllers
{

    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class UserController : Controller
    {

        public FileResult Picture(int id, int? s, int o = 0,int large=0)
        {
            try
            {
                UserOp userOp = new UserOp();
                var user = userOp.GetUser(id);
                if (user != null && user.PicName != null)
                {
                    /*byte[] picture = user.PicThumb == null ? user.Pic : user.PicThumb*/
                    byte[] picture =large==1? Codes.ConvertImageToArray(user.PicName,false): Codes.ConvertImageToArray(user.PicName);
                    /*if(o == 1)
                    {
                        picture = user.Pic;
                    }*/

                    if(picture != null)
                    {
                        string mimeType = Need.Codes.GeMimeTypeFromImageByteArray(picture);
                        /*byte[] pic = s != null ? Need.Codes.ResizeImage(picture, s.Value, s.Value) : picture;*/
                        return new FileStreamResult(new MemoryStream(picture), mimeType);
                    }
                }

                string path = Path.GetFullPath("wwwroot/imagepanel/icons/profile.png");
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, "image/png");
            }
            catch(Exception ex)
            {
                ex.ToSaveElmah();
                return null;
            }
        }

        [HttpPost]
        public IActionResult UserActive(string ids, string t = "Deactive", string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if(ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<User> users = userOp.GetUsers(ids2);
                if(users.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    ViewData["t"] = t;
                    return View("UserActive", users);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserActived(string ids, string t = "Deactive", string explain = "", string returnUrl = "")
        {
            Dictionary<string, int> t_active = new Dictionary<string, int>();
            t_active.Add("Inactive", 0);
            t_active.Add("Deactive", 1);
            t_active.Add("Active", 2);
            
            UserOp userOp = new UserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<User> users = new List<User>();
                foreach(var id in ids2)
                {
                    User user = userOp.GetUser(id);
                    if (user != null)
                    {
                        user.UserActive = t_active[t];
                        user.ReasonInactive = string.IsNullOrEmpty(explain) ? user.ReasonInactive : explain;
                        users.Add(user);                                                
                    }                                    
                }

                if(users.Count() > 0)
                {
                    int count = await userOp.UpdateUsers(users);
                    string msg = Need.Codes.GetResource("Users" + t);
                    msg = string.Format(msg, count);                    
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }          
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult UserType(string ids, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<User> users = userOp.GetUsers(ids2);
                if (users.Count() > 0)
                {
                    UserProperties userProperties = new UserProperties();
                    userProperties.UserTypes = (new UserTypeOp()).GetUserTypes();
                    userProperties.Users = (new UserOp()).GetUsers(ids2);
                    ViewData["returnUrl"] = returnUrl;                    
                    return View("UserType", userProperties);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserTyped(string ids, string uts, string returnUrl = "")
        {
           
            UserOp userOp = new UserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                List<int> uts2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(uts);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<User> users = new List<User>();                
                for(int i = 0; i < ids2.Count(); i++)
                {
                    User user = userOp.GetUser(ids2[i]);
                    if (user != null)
                    {
                        user.UserTypeId = uts2[i];
                        users.Add(user);
                    }
                }

                if (users.Count() > 0)
                {
                    int count = await userOp.UpdateUsers(users);
                    string msg = Need.Codes.GetResource("UsersChangeTypes");
                    msg = string.Format(msg, count);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" +  msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }


        [HttpPost]
        public IActionResult UserRelation(string ids, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<User> users = userOp.GetUsers(ids2);
                if (users.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("UserRelation", users);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult UserDelete(string ids, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<User> users = userOp.GetUsers(ids2);
                if (users.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;                    
                    return View("UserDelete", users);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserDeleted(string ids, string returnUrl = "")
        {
            UserOp userOp = new UserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<User> users = new List<User>();
                foreach (var id in ids2)
                {
                    User user = userOp.GetUser(id);
                    if (user != null)
                    {
                        users.Add(user);                        
                    }
                }

                if (users.Count() > 0)
                {
                    int count = await userOp.DeleteUsers(users);
                    string msg = Need.Codes.GetResource("UsersDelete");
                    msg = string.Format(msg, count);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult UserPassword(string ids, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<User> users = userOp.GetUsers(ids2);
                if (users.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;                    
                    return View("UserPassword", users);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserPassworded(string ids, string ps, string returnUrl = "")
        {
            UserOp userOp = new UserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                List<string> ps2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(ps);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<User> users = new List<User>();
                for (int i = 0; i < ids2.Count(); i++)
                {
                    User user = userOp.GetUser(ids2[i]);
                    if (user != null)
                    {
                        user.Password = string.IsNullOrEmpty(ps2[i]) ? "" : DatabaseLayer.Need.Codes.Hash(ps2[i]);
                        users.Add(user);
                    }
                }

                if (users.Count() > 0)
                {
                    int count = await userOp.UpdateUsers(users);
                    string msg = Need.Codes.GetResource("UsersChangePassword");
                    msg = string.Format(msg, count);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult UserPic(int id, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                User user = userOp.GetUser(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("UserPic2", user);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserPicCrop(int id, int top, int left, int width, int height, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            try
            {
                User user = userOp.GetUser(id);
                if (user != null)
                {
                   
                    if (user.PicName != null)
                    {
                        try
                        {
                            var pic = Codes.ConvertImageToArray(user.PicName,false);

                            Image image = Image.FromStream(new MemoryStream(pic));                            
                            /*int nesbat = 300;
                            int heightCropper = (nesbat * image.Height / image.Width);
                            left = (left * image.Width / nesbat);
                            top = (top * image.Height / nesbat);
                            width = (width * image.Width / nesbat);
                            height = (height * image.Height / nesbat);*/

                            string mimeType = Need.Codes.GeMimeTypeFromImageByteArray(pic);
                            var format = mimeType.Equals("image/png") ? ImageFormat.Png : ImageFormat.Jpeg;

                            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                            bmp.SetResolution(80, 60);

                            Graphics gfx = Graphics.FromImage(bmp);
                            gfx.SmoothingMode = SmoothingMode.AntiAlias;
                            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            gfx.DrawImage(image, new Rectangle(0, 0, width, height), top, left, width, height, GraphicsUnit.Pixel);

                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, format);

                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(user.PicName)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(user.PicName));
                            }

                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(user.PicName,false)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(user.PicName,false));
                            }

                            Image.FromStream(ms).Save(Codes.GetPathStoreProfileImage(user.PicName,false));

                            Codes.ResizeImage(ms.ToArray(), Codes.GetPathStoreProfileImage(user.PicName));

                            image.Dispose();
                            bmp.Dispose();
                            gfx.Dispose();

                            /*user.PicThumb = ms.ToArray();*/
                            /*await userOp.UpdateUser(user);*/
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    string msg = Need.Codes.GetResource("SuccessCropUserPic");
                    msg = string.Format(msg, user.FirstName + " " + user.LastName);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        

        [HttpPost]
        public IActionResult DownloadExcelDocument(int degreeId, int gradeId, int studyFieldId, string q, int usertypeid = 1)
        {
            UserOp userOp = new UserOp();
            List<User> users = new List<User>();
            if (usertypeid == 1)
            {
                users = userOp.GetUsers(degreeId, gradeId, studyFieldId, q, 1);
            }
            else if(usertypeid == 2 || usertypeid == 3)
            {
                users = userOp.GetUsers(q, new List<int> { 2, 3 });
            }
            else if (usertypeid == 4)
            {
                users = userOp.GetUsers(q, new List<int> { 4 });
            }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string sheetName = "دانش آموزان";
            //string fileName = "";
            string fileName = "Students_" + Need.Codes.getPersianDateTime() + ".xlsx";
            switch (usertypeid)
            {
                case 2:
                    fileName = "Parents_" + Need.Codes.getPersianDateTime() + ".xlsx";
                    sheetName = "والدین";
                    break;
                case 3:
                    fileName = "Parents_" + Need.Codes.getPersianDateTime() + ".xlsx";
                    sheetName = "والدین";
                    break;
                case 4:
                    fileName = "Teachers_" + Need.Codes.getPersianDateTime() + ".xlsx";
                    sheetName = "معلمان";
                    break;
            }
            try
            {
                var workbook = new ClosedXML.Excel.XLWorkbook();
                ClosedXML.Excel.IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);
                if(users.Count() > 0)
                {
                    AddWorkSheet(worksheet, users, usertypeid);
                }                

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public void AddWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, List<User> users, int usertypeid = 1)
        {
            if (usertypeid == 1)
            {
                worksheet.Cell(1, 1).Value = "کد ملی";
                worksheet.Cell(1, 2).Value = "نام";
                worksheet.Cell(1, 3).Value = "نام خانوادگی";
                worksheet.Cell(1, 4).Value = "سریال شناسنامه";
                worksheet.Cell(1, 5).Value = "محل صدور";
                worksheet.Cell(1, 6).Value = "تاریخ تولد";
                worksheet.Cell(1, 7).Value = "محل تولد";
                worksheet.Cell(1, 8).Value = "با چه کسی زندگی می کنید؟";
                worksheet.Cell(1, 9).Value = "شخصی که با آن زندگی میکنید";
                worksheet.Cell(1, 10).Value = "مقطع تحصیلی";
                worksheet.Cell(1, 11).Value = "پایه تحصیلی";
                worksheet.Cell(1, 12).Value = "رشته تحصیلی";
                worksheet.Cell(1, 13).Value = "تلفن";
                worksheet.Cell(1, 14).Value = "همراه";
                worksheet.Cell(1, 15).Value = "آدرس";
                for (int index = 1; index <= users.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = users[index - 1].Irnational;
                    worksheet.Cell(index + 1, 2).Value = users[index - 1].FirstName;
                    worksheet.Cell(index + 1, 3).Value = users[index - 1].LastName;
                    worksheet.Cell(index + 1, 4).Value = users[index - 1].IdcardSerial + " " + users[index - 1].IdcardSeriesLetter + " " + users[index - 1].IdcardSeriesNumber;
                    worksheet.Cell(index + 1, 5).Value = users[index - 1].IdcardPlace;
                    string birthdate = Need.Codes.getPersianDate(users[index - 1].BirthDate);
                    worksheet.Cell(index + 1, 6).Value = birthdate.Replace("/", "*");                    
                    worksheet.Cell(index + 1, 7).Value = users[index - 1].BirthPlace;
                    worksheet.Cell(index + 1, 8).Value = users[index - 1].LivePlace;
                    worksheet.Cell(index + 1, 9).Value = users[index - 1].LivePlaceOther;
                    worksheet.Cell(index + 1, 10).Value = users[index - 1].DegreeId != null ? users[index - 1].Degree.DegreeTitle : "";
                    worksheet.Cell(index + 1, 11).Value = users[index - 1].GradeId != null ? users[index - 1].Grade.GradeTitle : "";
                    worksheet.Cell(index + 1, 12).Value = users[index - 1].StudyFieldId != null ? users[index - 1].StudyField.StudyFieldTitle : "";
                    worksheet.Cell(index + 1, 13).Value = users[index - 1].Telephone;
                    worksheet.Cell(index + 1, 14).Value = users[index - 1].MobileNumber;                    
                    worksheet.Cell(index + 1, 15).Value = users[index - 1].Address;
                }
            }
            else
            {
                worksheet.Cell(1, 1).Value = "کد ملی";
                worksheet.Cell(1, 2).Value = "نام";
                worksheet.Cell(1, 3).Value = "نام خانوادگی";
                worksheet.Cell(1, 4).Value = "سریال شناسنامه";
                worksheet.Cell(1, 5).Value = "محل صدور";
                worksheet.Cell(1, 6).Value = "تاریخ تولد";
                worksheet.Cell(1, 7).Value = "محل تولد";
                worksheet.Cell(1, 8).Value = "تحصیلات";
                worksheet.Cell(1, 9).Value = "شغل";
                worksheet.Cell(1, 10).Value = "تلفن";
                worksheet.Cell(1, 11).Value = "همراه";
                worksheet.Cell(1, 12).Value = "آدرس";
                for (int index = 1; index <= users.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = users[index - 1].Irnational;
                    worksheet.Cell(index + 1, 2).Value = users[index - 1].FirstName;
                    worksheet.Cell(index + 1, 3).Value = users[index - 1].LastName;
                    worksheet.Cell(index + 1, 4).Value = users[index - 1].IdcardSerial + " " + users[index - 1].IdcardSeriesLetter + " " + users[index - 1].IdcardSeriesNumber;
                    worksheet.Cell(index + 1, 5).Value = users[index - 1].IdcardPlace;
                    string birthdate = Need.Codes.getPersianDate(users[index - 1].BirthDate);
                    worksheet.Cell(index + 1, 6).Value = birthdate.Replace("/", "*");
                    worksheet.Cell(index + 1, 7).Value = users[index - 1].BirthPlace;
                    worksheet.Cell(index + 1, 8).Value = users[index - 1].ParentDegree;
                    worksheet.Cell(index + 1, 9).Value = users[index - 1].ParentJob;
                    worksheet.Cell(index + 1, 10).Value = users[index - 1].Telephone;
                    worksheet.Cell(index + 1, 11).Value = users[index - 1].MobileNumber;
                    worksheet.Cell(index + 1, 12).Value = users[index - 1].Address;
                }
            }
        }



    }
}