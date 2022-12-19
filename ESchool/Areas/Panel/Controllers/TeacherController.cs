using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using APISms.Need;
using DatabaseLayer.Access;
using DatabaseLayer.Models;
using ESchool.Models;
using ESchool.Need;
using ESchool.Need.CutomFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Codes = ESchool.Need.Codes;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class TeacherController : Controller
    {
        
        public IActionResult Index(string q, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                ViewData["Title"] = "معلمان";
                List<User> users = (new UserOp()).GetUsers(q, new List<int> { 4 });
                ViewData["MaxRows"] = users.Count();
                ViewData["CountRow"] = CountRow;
                int skip = (pagenumber - 1) * CountRow;
                return View(users.Skip(skip).Take(CountRow).ToList());
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }


        public IActionResult New(int? id, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            ViewBag.IdcardSeriesLetters = Need.SelectList.getIdcardSeriesLetters();
            ViewBag.LivePlaces = Need.SelectList.getLivePlaces();
            ViewBag.ParentStatuses = Need.SelectList.getParentStatuses();
            ViewBag.Educations = Need.SelectList.getEducations();
            ViewBag.UserTypes = Need.SelectList.getUserTypes();

            try
            {
                if (id == null)
                {
                    User user = new User();
                    ViewBag.Days = Need.SelectList.getDays();
                    ViewBag.Months = Need.SelectList.getMonths();
                    ViewData["returnUrl"] = returnUrl;
                    return View("TeacherNew", user);
                }
                else
                {
                    User user = userOp.GetUser(id.Value);
                    if (user != null)
                    {
                        string Day = "01";
                        string Month = "01";
                        string Year = "";
                        if (user.BirthDate != null)
                        {
                            string[] persiandate = Need.Codes.getPersianDate(user.BirthDate).Split("/");
                            Day = persiandate[2];
                            Month = persiandate[1];
                            Year = persiandate[0];
                        }
                        ViewBag.Days = Need.SelectList.getDays(Day);
                        ViewBag.Months = Need.SelectList.getMonths(Month);
                        ViewData["BirthYear"] = Year;
                        ViewData["returnUrl"] = returnUrl;
                        return View("TeacherNew", user);
                    }
                }

                return Content(Need.Codes.GetResource("NoPersonFound"));
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Newed(User user, string BirthDay, string BirthMonth, string BirthYear, IFormFile file, string returnUrl = "")
        {
            if (user == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }

            if (!Checks.CheckCodelMeli(user.Irnational))
            {
                return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "معلم"));
            }
            if (!Checks.CheckMobile(user.MobileNumber))
            {
                return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "معلم"));
            }
            if (!Checks.CheckPersianDate(BirthYear, BirthMonth, BirthDay))
            {
                return Content(Need.Codes.GetResource("InvalidBirthDate").Replace("N", "معلم"));
            }

            /*byte[] pic = null;
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    pic = fileBytes;
                }
            }*/

            string picName = null;
            if (file != null)
            {
                var fileNameNew = Codes.GetFileNameNew(file.FileName);


                using (var output = new FileStream(Codes.GetPathStoreProfileImage(fileNameNew,false), FileMode.Create))
                {
                    await file.CopyToAsync(output);
                }

                Codes.ResizeImage(file, Codes.GetPathStoreProfileImage(fileNameNew, true));


                picName = fileNameNew;
            }

            UserOp userOp = new UserOp();

            try
            {
                if (user.UserId == 0)
                {
                    User ExistUser = userOp.GetUser(user.Irnational);
                    if (ExistUser != null)
                    {
                        string m = Need.Codes.GetResource("CorrectIRNationalHasPerson").Replace("N", ExistUser.FirstName + " " + ExistUser.LastName);
                        return Content(m);
                    }

                    /*user.Pic = pic == null ? user.Pic : pic;
                    user.PicThumb = pic == null ? user.PicThumb : pic;*/
                    user.PicName = picName == null ? user.PicName : picName;
                    user.BirthDate = Need.Codes.PersianDateToDate(BirthYear, BirthMonth, BirthDay).Value;
                    user.Password = "";
                    user.UserTypeId = 4;
                    string count = await userOp.InsertUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("SuccessRegister") : returnUrl + "?msg=" + Need.Codes.GetResource("SuccessRegister");
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
                }
                else
                {
                    User CurrentUser = userOp.GetUser(user.UserId);
                    if (CurrentUser != null)
                    {
                        if (picName != null)
                        {
                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(CurrentUser.PicName)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(CurrentUser.PicName));
                            }

                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(CurrentUser.PicName, false)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(CurrentUser.PicName, false));
                            }
                        }
                        /*CurrentUser.Pic = pic == null ? CurrentUser.Pic : pic;
                        CurrentUser.PicThumb = pic == null ? CurrentUser.PicThumb : pic;*/
                        CurrentUser.PicName = picName == null ? CurrentUser.PicName : picName;
                        CurrentUser.BirthDate = Need.Codes.PersianDateToDate(BirthYear, BirthMonth, BirthDay).Value;
                        CurrentUser.Irnational = user.Irnational;
                        CurrentUser.FirstName = user.FirstName;
                        CurrentUser.LastName = user.LastName;
                        CurrentUser.IdcardSerial = user.IdcardSerial;
                        CurrentUser.IdcardSeriesLetter = user.IdcardSeriesLetter;
                        CurrentUser.IdcardSeriesNumber = user.IdcardSeriesNumber;
                        CurrentUser.IdcardPlace = user.IdcardPlace;
                        CurrentUser.BirthPlace = user.BirthPlace;
                        //CurrentUser.LivePlace = user.LivePlace;
                        //CurrentUser.LivePlaceOther = user.LivePlaceOther;
                        CurrentUser.Telephone = user.Telephone;
                        CurrentUser.MobileNumber = user.MobileNumber;
                        CurrentUser.Address = user.Address;
                        //CurrentUser.UserTypeId = user.UserTypeId;
                        CurrentUser.ParentJob = user.ParentJob;
                        CurrentUser.ParentStatusId = user.ParentStatusId;
                        CurrentUser.ParentDegree = user.ParentDegree;

                        string count = await userOp.UpdateUser(CurrentUser);
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
                        returnUrl = returnUrl + "&m=Success";
                        return Content("ok," + returnUrl);
                    }
                    else
                    {
                        return Content(Need.Codes.GetResource("NoPersonFound"));
                    }


                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public IActionResult TeacherCourse(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUserWithParent(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;                    
                    RoomProperties roomProperties = new RoomProperties();
                    roomProperties.Degrees = (new DegreeOp()).GetDegrees();
                    roomProperties.Grades = (new GradeOp()).GetGrades();
                    roomProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();                    

                    ViewData["UserId"] = user.UserId;
                    ViewData["NickName"] = user.FirstName + " " + user.LastName;
                    List<SelectListItem> courses = (new CourseOp()).GetCourses("").Select(p => new SelectListItem { Text = p.CourseTitle, Value = p.CourseId.ToString() }).ToList();
                    string courseid = Request.Query["course"];
                    if(!string.IsNullOrEmpty(courseid))
                    {
                        var course = courses.Where(p => p.Value == courseid);
                        if(course.Count() > 0)
                        {
                            course.First().Selected = true;
                        }
                    }
                    ViewBag.Courses = courses;
                    ViewBag.TeacherCourses = (new TeacherCourseOp()).GetTeacherCourses(id).ToList();
                    return View("Course", roomProperties);
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
        public async Task<IActionResult> TeacherCourse(int degree, int grade, int studyField, int course, int id, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUserWithParent(id);
                if (user != null)
                {
                    TeacherCourseOp teacherCourseOp = new TeacherCourseOp();
                    TeacherCourse teacherCourse = new TeacherCourse();
                    teacherCourse.UserId = id;
                    teacherCourse.CourseId = course;
                    if(degree > 0)
                    {
                        teacherCourse.DegreeId = degree;
                    }
                    if (grade > 0)
                    {
                        teacherCourse.GradeId = grade;
                    }
                    if (studyField > 0)
                    {
                        teacherCourse.StudyFieldId = studyField;
                    }

                    int? DegreeId = null;
                    if(degree > 0)
                    {
                        DegreeId = degree;
                    }
                    int? GradeId = null;
                    if (grade > 0)
                    {
                        GradeId = grade;
                    }
                    int? StudyFieldId = null;
                    if (studyField > 0)
                    {
                        StudyFieldId = studyField;
                    }


                    string url = "/Panel/Teacher/TeacherCourse?id=" + id + "&returnUrl=" + returnUrl;

                    if (teacherCourseOp.HasCourse(user.UserId, course, DegreeId, GradeId, StudyFieldId))
                    {
                        url = url + "&msg=" + "این درس برای معلم " + user.FirstName + " " + user.LastName + " ثبت شده است";
                        url = url + "&m=Error";
                        return Redirect(url);
                    }

                    
                    string r = await teacherCourseOp.InsertTeacherCourse(teacherCourse);
                    if(!r.StartsWith("Error"))
                    {
                        
                        url = url + "&msg=" + Need.Codes.GetResource("SuccessAddCourse");
                        url = url + "&m=Success";
                    }
                    else
                    {
                        url = url + "&msg=" + Need.Codes.GetResource("Error");
                        url = url + "&m=Error";
                    }
                    url += "&course=" + course;
                    return Redirect(url);
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
        public IActionResult TeacherCourseDelete(string ids, string returnUrl = "")
        {
            TeacherCourseOp teacherCourseOp = new TeacherCourseOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<TeacherCourse> teacherCourses = teacherCourseOp.GetTeacherCourses(ids2);
                if (teacherCourses.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;                    
                    return View("TeacherCourseDelete", teacherCourses);
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
        public async Task<IActionResult> TeacherCourseDeleted(string ids, string returnUrl = "")
        {
            TeacherCourseOp teacherCourseOp = new TeacherCourseOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<TeacherCourse> teacherCourses = new List<TeacherCourse>();
                foreach (var id in ids2)
                {
                    TeacherCourse teacherCourse = teacherCourseOp.GetTeacherCourse(id);
                    if (teacherCourse != null)
                    {
                        teacherCourses.Add(teacherCourse);
                    }
                }

                if (teacherCourses.Count() > 0)
                {
                    int count = await teacherCourseOp.DeleteTeacherCourses(teacherCourses);
                    string msg = Need.Codes.GetResource("ItemsDelete");
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


        //***************************
        public IActionResult RoomTeacher(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUserWithParent(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    RoomProperties roomProperties = new RoomProperties();
                    roomProperties.RoomTeachers = (new RoomTeacherOp()).GetRoomTeachers(id);

                    ViewData["UserId"] = user.UserId;
                    ViewData["NickName"] = user.FirstName + " " + user.LastName;
                    List<SelectListItem> courses = (new CourseOp()).GetCourses("").Select(p => new SelectListItem { Text = p.CourseTitle, Value = p.CourseId.ToString() }).ToList();
                    List<SelectListItem> rooms = (new RoomOp()).GetRooms().Select(p => new SelectListItem { Text = p.RoomTitle, Value = p.RoomId.ToString() }).ToList();
                    string courseid = Request.Query["course"];
                    if (!string.IsNullOrEmpty(courseid))
                    {
                        var course = courses.Where(p => p.Value == courseid);
                        if (course.Count() > 0)
                        {
                            course.First().Selected = true;
                        }
                    }
                    string roomid = Request.Query["room"];
                    if (!string.IsNullOrEmpty(roomid))
                    {
                        var room = rooms.Where(p => p.Value == roomid);
                        if (room.Count() > 0)
                        {
                            room.First().Selected = true;
                        }
                    }
                    ViewBag.Courses = courses;
                    ViewBag.Rooms = rooms;
                    return View("Room", roomProperties);
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
        public async Task<IActionResult> RoomTeacher(int room, int course, int id, string jitsiaddress, string jitsipassword, string adobeaddress, string adobeusername, string adobepassword, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUserWithParent(id);
                if (user != null)
                {
                    RoomTeacherOp roomTeacherOp = new RoomTeacherOp();
                    RoomTeacher roomTeacher = new RoomTeacher();
                    roomTeacher.UserId = id;
                    roomTeacher.CourseId = course;
                    roomTeacher.RoomId = room;
                    roomTeacher.JitsiLiveAddress = jitsiaddress;
                    roomTeacher.JitsiLivePassword = jitsipassword;
                    roomTeacher.AdobeLiveAddress = adobeaddress;
                    roomTeacher.AdobeLiveUsername = adobeusername;
                    roomTeacher.AdobeLivePass = adobepassword;
                    string url = "/Panel/Teacher/RoomTeacher?id=" + id + "&returnUrl=" + returnUrl;

                    if (roomTeacherOp.HasTeacher(user.UserId, room, course))
                    {
                        url = url + "&msg=" + "این کلاس برای معلم " + user.FirstName + " " + user.LastName + " ثبت شده است";
                        url = url + "&m=Error";
                        return Redirect(url);
                    }

                    string r = await roomTeacherOp.InsertRoomTeacher(roomTeacher);
                    if (!r.StartsWith("Error"))
                    {
                        url += "&msg=" + Need.Codes.GetResource("SuccessAddCourse");                        
                        url += "&m=Success";
                    }
                    else
                    {
                        url += "&msg=" + Need.Codes.GetResource("Error");                        
                        url += "&m=Error";                    
                    }

                    url += "&room=" + room;
                    url += "&course=" + course;
                    url += "&jitsiaddress=" + jitsiaddress;
                    url += "&jitsipassword=" + jitsipassword;
                    url += "&adobeaddress=" + adobeaddress;
                    url += "&adobeusername=" + adobeusername;
                    url += "&adobepassword=" + adobepassword;

                    return Redirect(url);
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
        public IActionResult RoomTeacherDelete(string ids, string returnUrl = "")
        {
            RoomTeacherOp roomTeacherOp = new RoomTeacherOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<RoomTeacher> roomTeachers = roomTeacherOp.GetRoomTeachers(ids2);
                if (roomTeachers.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomTeacherDelete", roomTeachers);
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
        public async Task<IActionResult> RoomTeacherDeleted(string ids, string returnUrl = "")
        {
            RoomTeacherOp teacherCourseOp = new RoomTeacherOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<RoomTeacher> teacherCourses = new List<RoomTeacher>();
                foreach (var id in ids2)
                {
                    RoomTeacher teacherCourse = teacherCourseOp.GetRoomTeacher(id);
                    if (teacherCourse != null)
                    {
                        teacherCourses.Add(teacherCourse);
                    }
                }

                if (teacherCourses.Count() > 0)
                {
                    int count = await teacherCourseOp.DeleteRoomTeachers(teacherCourses);
                    string msg = Need.Codes.GetResource("ItemsDelete");
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


        public IActionResult TeacherPersonnel(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUser(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("TeacherPersonnel", user);
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
        public async Task<IActionResult> TeacherPersonneled(int id,int ispersonnel, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUser(id);
                if (user != null)
                {
                    user.IsPersonnel = (ispersonnel == 1 ? true : false);
                    await userOp.UpdateUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
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

    }
}