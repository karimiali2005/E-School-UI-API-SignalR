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
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public IActionResult Index(string StartDay, string StartMonth, string StartYear, string FinishDay, string FinishMonth, string FinishYear, int degree, int grade, int studyField, string q, int roomtype, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                DateTime start = Checks.CheckPersianDate(StartYear, StartMonth, StartDay) ? Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value : DateTime.Now.AddDays(-480);
                DateTime finish = Checks.CheckPersianDate(FinishYear, FinishMonth, FinishDay) ? Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value : DateTime.Now.AddDays(420);

                string Day = "01";
                string Month = "01";
                string Year = "";

                string[] persiandate = Need.Codes.getPersianDate(start).Split("/");
                Day = persiandate[2];
                Month = persiandate[1];
                Year = persiandate[0];
                ViewData["StartYear"] = Year;
                ViewBag.DaysStart = Need.SelectList.getDays(Day);
                ViewBag.MonthsStart = Need.SelectList.getMonths(Month);

                string[] persiandate2 = Need.Codes.getPersianDate(finish).Split("/");
                Day = persiandate2[2];
                Month = persiandate2[1];
                Year = persiandate2[0];
                ViewData["FinishYear"] = Year;
                ViewBag.DaysFinish = Need.SelectList.getDays(Day);
                ViewBag.MonthsFinish = Need.SelectList.getMonths(Month);


                ViewData["Title"] = "کلاس ها و جلسات";
                List<Room> rooms = (new RoomOp()).GetRooms(degree, grade, studyField, q, roomtype, start, finish);
                ViewData["MaxRows"] = rooms.Count();
                ViewData["CountRow"] = CountRow;

                RoomProperties roomProperties = new RoomProperties();
                roomProperties.Degrees = (new DegreeOp()).GetDegrees();
                roomProperties.Grades = (new GradeOp()).GetGrades();
                roomProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
                roomProperties.RoomTypes = (new RoomTypeOp()).GetRoomTypes();
                int skip = (pagenumber - 1) * CountRow;
                roomProperties.Rooms = rooms.Skip(skip).Take(CountRow).ToList();

                return View(roomProperties);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }


        public IActionResult New(int id, string returnUrl = "")
        {
            string Title = "کلاس/متفرقه جدید";            

            RoomProperties roomProperties = new RoomProperties();
            roomProperties.Degrees = (new DegreeOp()).GetDegrees();
            roomProperties.Grades = (new GradeOp()).GetGrades();
            roomProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
            roomProperties.RoomTypes = (new RoomTypeOp()).GetRoomTypes();

            try
            {
                if (id > 0)
                {
                    RoomOp roomOp = new RoomOp();
                    var room = roomOp.GetRoom(id);
                    if(room == null)
                    {
                        return Content(Need.Codes.GetResource("NoRoomFound"));
                    }
                    else
                    {
                        List<Room> rooms = new List<Room>();
                        rooms.Add(room);
                        roomProperties.Rooms = rooms;
                        ViewBag.CloseOnTime = Need.SelectList.getBoolean(room.CloseOnTime.ToString());
                        ViewBag.CloseChat = Need.SelectList.getBoolean(room.CloseChat.ToString());
                        ViewBag.PermissionCloseChat = Need.SelectList.getBoolean(room.PermissionCloseChat.ToString());
                        ViewBag.PermissionStudentChatEdit = Need.SelectList.getBoolean(room.PermissionStudentChatEdit.ToString());
                        ViewBag.PermissionStudentChatDelete = Need.SelectList.getBoolean(room.PermissionStudentChatDelete.ToString());                        

                        string Day = "01";
                        string Month = "01";
                        string Year = "";

                        if (room.RoomStartDate != null)
                        {
                            string[] persiandate = Need.Codes.getPersianDate(room.RoomStartDate).Split("/");
                            Day = persiandate[2];
                            Month = persiandate[1];
                            Year = persiandate[0];
                            ViewData["StartYear"] = Year;
                        }
                        ViewBag.DaysStart = Need.SelectList.getDays(Day);
                        ViewBag.MonthsStart = Need.SelectList.getMonths(Month);

                        if (room.RoomFinishDate != null)
                        {
                            string[] persiandate2 = Need.Codes.getPersianDate(room.RoomFinishDate).Split("/");
                            Day = persiandate2[2];
                            Month = persiandate2[1];
                            Year = persiandate2[0];
                            ViewData["FinishYear"] = Year;
                        }
                        ViewBag.DaysFinish = Need.SelectList.getDays(Day);
                        ViewBag.MonthsFinish = Need.SelectList.getMonths(Month);

                        Title = " ویرایش " + room.RoomType.RoomTypeTitle + " " + room.RoomTitle;
                        ViewData["Title"] = Title;
                        ViewData["returnUrl"] = returnUrl;
                        return View("RoomNew", roomProperties);
                    }
                }
                else
                {
                    List<Room> rooms = new List<Room>();
                    roomProperties.Rooms = rooms;
                    ViewBag.CloseOnTime = Need.SelectList.getBoolean();
                    ViewBag.CloseChat = Need.SelectList.getBoolean();
                    ViewBag.PermissionCloseChat = Need.SelectList.getBoolean();
                    ViewBag.PermissionStudentChatEdit = Need.SelectList.getBoolean();
                    ViewBag.PermissionStudentChatDelete = Need.SelectList.getBoolean();
                    string[] persiandate = Need.Codes.getPersianDate().Split("/");
                    ViewBag.DaysStart = Need.SelectList.getDays("01");
                    ViewBag.MonthsStart = Need.SelectList.getMonths(persiandate[1]);                    
                    ViewBag.DaysFinish = Need.SelectList.getDays("21");
                    ViewBag.MonthsFinish = Need.SelectList.getMonths(persiandate[1]);
                    ViewData["StartYear"] = persiandate[0];
                    ViewData["FinishYear"] = persiandate[0];
                    ViewData["Title"] = Title;
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomNew", roomProperties);                    
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Newed(int id, string roomTitle, bool closeOnTime, int Degree, int Grade, int StudyField, int roomtype, string StartDay, string StartMonth, string StartYear, string FinishDay, string FinishMonth, string FinishYear, bool CloseChat, bool PermissionCloseChat, bool PermissionStudentChatEdit, bool PermissionStudentChatDelete, string returnUrl = "")
        {
            if (string.IsNullOrEmpty(roomTitle))
            {
                return Content("نام کلاس/سخنرانی الزامی می باشد");
            }            
            if(id == 0)
            {
                if (!Checks.CheckPersianDate(StartYear, StartMonth, StartDay))
                {
                    return Content(Need.Codes.GetResource("InvalidDate").Replace("N", "شروع"));
                }
                if (!Checks.CheckPersianDate(FinishYear, FinishMonth, FinishDay))
                {
                    return Content(Need.Codes.GetResource("InvalidDate").Replace("N", "پایان"));
                }
                if (Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value > Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value)
                {
                    return Content("تاریخ شروع نباید بعد از تاریخ پایان باشد");
                }
            }

            RoomOp roomOp = new RoomOp();            
            RoomTypeOp roomTypeOp = new RoomTypeOp();

            if(id > 0)
            {
                var room = roomOp.GetRoom(id);
                if(room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                room.RoomId = id;
                room.RoomTitle = roomTitle;
                room.CloseOnTime = closeOnTime;
                room.CloseChat = CloseChat;
                room.PermissionCloseChat = PermissionCloseChat;
                room.PermissionStudentChatEdit = PermissionStudentChatEdit;
                room.PermissionStudentChatDelete = PermissionStudentChatDelete;
                if (!roomOp.HasRoomDetails(id))
                {
                    room.RoomStartDate = Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value;
                    room.RoomFinishDate = Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value;
                    room.RoomTypeId = roomtype;
                    if (Degree > 0)
                    {
                        room.DegreeId = Degree;
                    }
                    else
                    {
                        room.DegreeId = null;
                    }
                    if (Degree > 0 && Grade > 0)
                    {
                        room.GradeId = Grade;
                    }
                    else
                    {
                        room.GradeId = null;
                    }
                    if (Degree > 0 && Grade > 0 && StudyField > 0)
                    {
                        room.StudyFieldId = StudyField;
                    }
                    else
                    {
                        room.StudyFieldId = null;
                    }
                }
                int count = await roomOp.UpdateRoom(room);
                string msg = roomTypeOp.GetRoomType(room.RoomTypeId).RoomTypeTitle + " " + roomTitle + " با موفقیت ویرایش شد ";
                returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                returnUrl = returnUrl + "&m=Success";
                return Content("ok," + returnUrl);
            }
            else
            {
                Room room = new Room();
                room.RoomTitle = roomTitle;
                room.CloseOnTime = closeOnTime;
                room.CloseChat = CloseChat;
                room.PermissionCloseChat = PermissionCloseChat;
                room.PermissionStudentChatEdit = PermissionStudentChatEdit;
                room.PermissionStudentChatDelete = PermissionStudentChatDelete;
                room.RoomStartDate = Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value;
                room.RoomFinishDate = Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value;
                room.RoomTypeId = roomtype;
                if (Degree > 0)
                {
                    room.DegreeId = Degree;
                }
                else
                {
                    room.DegreeId = null;
                }
                if (Grade > 0)
                {
                    room.GradeId = Grade;
                }
                else
                {
                    room.GradeId = null;
                }
                if (StudyField > 0)
                {
                    room.StudyFieldId = StudyField;
                }
                else
                {
                    room.StudyFieldId = null;
                }
                string count = await roomOp.InsertRoom(room);
                string msg = roomTypeOp.GetRoomType(roomtype).RoomTypeTitle + " " + roomTitle + " با موفقیت ایجاد شد ";
                returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                returnUrl = returnUrl + "&m=Success";
                return Content("ok," + returnUrl);
            }
        }

        [HttpPost]
        public  IActionResult UpdateRoomChatGroup(string returnUrl = "")
        {
            _roomService.RoomChatGroupUpdate();
            string msg = " با موفقیت بروز رسانی شد ";
            returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
            returnUrl = returnUrl + "&m=Success";
            return Content("ok," + returnUrl);
            
        }

        [HttpPost]
        public IActionResult RoomUser(int id, string returnUrl = "")
        {
            string Title = "";

            RoomProperties roomProperties = new RoomProperties();

            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            try
            {
                IEnumerable<Room> rooms = roomOp.GetRooms(new List<int> { id });
                if (rooms.Count() > 0)
                {
                    var room = rooms.First();
                    UserOp userOp = new UserOp();
                    /*int degreeid = room.DegreeId == null ? 0 : room.DegreeId.Value;
                    int gradeid = room.GradeId == null ? 0 : room.GradeId.Value;
                    int studyfieldid = room.StudyFieldId == null ? 0 : room.StudyFieldId.Value;

                    if (room.RoomTypeId == 1)
                    {
                        roomProperties.Users = userOp.GetUsers(degreeid, gradeid, studyfieldid, "", 1);                        
                    }
                    else
                    {
                        List<User> usersList = new List<User>();
                        var users = userOp.GetUsers(degreeid, gradeid, studyfieldid, "", 1);
                        foreach(var user in users)
                        {
                            if(user.UserIdfather != null)
                            {
                                User user1 = userOp.GetUser(user.UserIdfather.Value);
                                usersList.Add(user1);
                            }
                            if (user.UserIdmother != null)
                            {
                                User user2 = userOp.GetUser(user.UserIdmother.Value);
                                usersList.Add(user2);
                            }
                        }
                        roomProperties.Users = usersList.AsEnumerable();
                    }*/

                    roomProperties.Users = userOp.GetUsers("", new List<int> { 1, 2, 3, 4 });
                    roomProperties.Rooms = rooms.ToList();
                    roomProperties.RoomUsers = roomUserOp.GetRoomUsers(id);

                    Title = room.RoomTypeId == 1 ? " دانش آموزان کلاس " : " کاربران سخنرانی ";
                    ViewData["Title"] = Title + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomUser", roomProperties);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }                
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult RoomChatGroup(int id, string returnUrl = "")
        {
            string Title = "";

            RoomProperties roomProperties = new RoomProperties();

            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            try
            {
                IEnumerable<Room> rooms = roomOp.GetRooms(new List<int> { id });
                if (rooms.Count() > 0)
                {
                    var room = rooms.First();
      
                    roomProperties.RoomChatGroups = roomOp.GetRoomChatGroup(id);
                    roomProperties.Rooms = rooms.ToList();
                    Title = room.RoomTypeId == 1 ? " دانش آموزان کلاس " : " کاربران سخنرانی ";
                    ViewData["Title"] = Title + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomChatGroup", roomProperties);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RoomChatGroupSet(string ids, int id, string returnUrl = "")
        {
            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                var roomChatGroup = roomOp.GetRoomChatGroup(id).ToList();
                if (roomChatGroup != null)
                {
                 

                    if (ids2.Count() > 0)
                    {
                        
                        foreach(var chatGroup in roomChatGroup)
                        {
                            if (ids2.Exists(i => i == chatGroup.RoomChatGroupId))
                                chatGroup.RoomChatGroupVisible = true;
                            else
                                chatGroup.RoomChatGroupVisible = false;
                        }
                      

                        string count = await roomUserOp.UpdateRoomChatGroup(roomChatGroup);

                        string msg = Need.Codes.GetResource("ChangeSuccess");
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var roomChatGroupNew = roomChatGroup.Where(r => r.RoomChatGroupVisible == true);
                        if (roomChatGroupNew != null)
                        {
                            foreach (var chatGroup in roomChatGroupNew)
                            {
                                chatGroup.RoomChatGroupVisible = false;
                            }
                            string count = await roomUserOp.UpdateRoomChatGroup(roomChatGroup);
                        }
                        string msg = Need.Codes.GetResource("ChangeSuccess");
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Redirect(returnUrl);
                    }
                    

                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }
        public IActionResult RoomUserCurrent(int id, string returnUrl = "")
        {
            string Title = "";

            RoomProperties roomProperties = new RoomProperties();

            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            try
            {
                IEnumerable<Room> rooms = roomOp.GetRooms(new List<int> { id });
                if (rooms.Count() > 0)
                {
                    
                    var room = rooms.First();
                    UserOp userOp = new UserOp();
                    roomProperties.UserCurrentViewModels = roomUserOp.GetRoomsUserCurrent(id);

                    roomProperties.Rooms = rooms.ToList();
                 

                    Title = room.RoomTypeId == 1 ? " دانش آموزان کلاس " : " کاربران سخنرانی ";
                    ViewData["Title"] = Title + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomUserCurrent", roomProperties);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoomUsered(string ids, int id, string returnUrl = "")
        {
            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                var room = roomOp.GetRoom(id);
                if (room != null)
                {
                    await roomOp.DeleteRoomUsers(id);

                    if(ids2.Count() > 0)
                    {
                        List<RoomUser> roomUsers = new List<RoomUser>();
                        foreach (var i in ids2)
                        {
                            roomUsers.Add(new RoomUser { RoomId = room.RoomId, UserId = i });
                        }

                        string count = await roomUserOp.InsertRoomUsers(roomUsers);

                        string msg = Need.Codes.GetResource("ChangeSuccess");
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        string msg = " کلیه کاربران از " + room.RoomType.RoomTypeTitle + " " + room.RoomTitle + " حذف شدند ";
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult RoomDelete(string ids, string returnUrl = "")
        {
            RoomOp roomOp = new RoomOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                IEnumerable<Room> rooms = roomOp.GetRooms(ids2);
                if (rooms.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomDelete", rooms);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoomDeleted(string ids, string returnUrl = "")
        {
            RoomOp roomOp = new RoomOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                List<Room> rooms = new List<Room>();
                foreach (var id in ids2)
                {
                    Room room = roomOp.GetRoom(id);
                    if (room != null)
                    {
                        rooms.Add(room);
                    }
                }

                if (rooms.Count() > 0)
                {
                    await roomOp.DeleteRoomDetails(ids2);
                    await roomOp.DeleteRoomUsers(ids2);

                    int count = 0;
                    foreach(var room in rooms)
                    {
                        string r = await roomOp.DeleteRoom(room.RoomId);
                        if(!r.StartsWith("Error"))
                        {
                            count++;
                        }
                    }
                    string msg = Need.Codes.GetResource("ItemsDelete");
                    msg = string.Format(msg, count);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        //***************************
        public IActionResult Live(int id, string returnUrl = "")
        {
            try
            {
                Room room = (new RoomOp()).GetRoom(id);
                if (room != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    RoomProperties roomProperties = new RoomProperties();
                    roomProperties.RoomTeachers = (new RoomTeacherOp()).GetRoomTeachersByRoomId(id);

                    ViewData["RoomId"] = room.RoomId;
                    ViewData["RoomTitle"] = room.RoomTitle;
                    /*List<SelectListItem> teachers = (new UserOp()).GetUsersWityUserType(4).Select(p => new SelectListItem { Text = p.FirstName + " " + p.LastName, Value = p.UserId.ToString() }).ToList();
                    
                    string teacherid = Request.Query["teacher"];
                    if (!string.IsNullOrEmpty(teacherid))
                    {
                        var teacher = teachers.Where(p => p.Value == teacherid);
                        if (teacher.Count() > 0)
                        {
                            teacher.First().Selected = true;
                        }
                    }
                    ViewBag.Teachers = teachers;*/
                    return View("Live", roomProperties);
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
        public async Task<IActionResult> Live(/*int teacher, int course,*/ int id, string jitsiaddress, string jitsipassword, string adobeaddress, string adobeusername, string adobepassword, string examaddress, string examaddress2, string zoomaddress, string returnUrl = "")
        {
            try
            {
                RoomOp roomOp = new RoomOp();
                Room room = roomOp.GetRoom(id);
                if (room != null)
                {
                    RoomTeacherOp roomTeacherOp = new RoomTeacherOp();
                    RoomTeacher roomTeacher = new RoomTeacher();
                    //roomTeacher.UserId = teacher;
                    //roomTeacher.CourseId = course;
                    roomTeacher.RoomId = id;
                    roomTeacher.JitsiLiveAddress = jitsiaddress;
                    roomTeacher.JitsiLivePassword = jitsipassword;
                    roomTeacher.AdobeLiveAddress = adobeaddress;
                    roomTeacher.AdobeLiveUsername = adobeusername;
                    roomTeacher.AdobeLivePass = adobepassword;
                    roomTeacher.ExamAddress = examaddress;
                    roomTeacher.ExamAddress2 = examaddress2;
                    roomTeacher.ZoomAddress = zoomaddress;
                    string url = "/Panel/Room/Live?id=" + id + "&returnUrl=" + returnUrl;

                    //if (roomTeacherOp.HasTeacher(teacher, id))
                    //{
                    //    User user = (new UserOp()).GetUser(teacher);
                    //    url = url + "&msg=" + "این کلاس برای معلم " + user.FirstName + " " + user.LastName + " ثبت شده است";
                    //    url = url + "&m=Error";
                    //    return Redirect(url);
                    //}

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

                    //url += "&teacher=" + teacher;
                    //url += "&course=" + course;
                    url += "&jitsiaddress=" + jitsiaddress;
                    url += "&jitsipassword=" + jitsipassword;
                    url += "&adobeaddress=" + adobeaddress;
                    url += "&adobeusername=" + adobeusername;
                    url += "&adobepassword=" + adobepassword;
                    url += "&examaddress=" + examaddress;
                    url += "&zoomaddress=" + examaddress;

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

        public IActionResult ReportCard(int roomId,int userId, string returnUrl = "")
        {
            try
            {
                var roomUser = (new RoomUserOp()).GetRoomUser(roomId,userId);
                if (roomUser != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("ReportCard", roomUser);
                }
                else
                {
                    var roomUserNew = new RoomUser()
                    {
                        RoomId =roomId,
                        UserId =userId 
                    };
                    ViewData["returnUrl"] = returnUrl;
                    return View("ReportCard", roomUser);
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReportCard(int roomId, int userId, string reportCardAddress, string returnUrl = "")
        {
            try
            {
                RoomUserOp roomOp = new RoomUserOp();
                var roomUser = (new RoomUserOp()).GetRoomUser(roomId, userId);
                if (roomUser != null)
                {

                    roomUser.ReportCardAddress = reportCardAddress;
                    await roomOp.UpdateRoomUser(roomUser);
                }
                returnUrl = returnUrl + "&m=Success";
                return Content("ok," + returnUrl);

                /*return RedirectToAction("RoomUserCurrent",new {id=roomUser.RoomId,returnUrl= "/Panel/Room/" });*/
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

    }
}