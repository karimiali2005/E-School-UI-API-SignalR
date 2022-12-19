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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenXmlPowerTools;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class RoomDetailController : Controller
    {
        public IActionResult Index(int id, string q, string Week, string StartDay, string StartMonth, string StartYear, string FinishDay, string FinishMonth, string FinishYear, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                Room room = (new RoomOp()).GetRoom(id);
                List<Room> rooms = new List<Room>();
                rooms.Add(room);

                DateTime start = Checks.CheckPersianDate(StartYear, StartMonth, StartDay) ? Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value : room.RoomStartDate;
                DateTime finish = Checks.CheckPersianDate(FinishYear, FinishMonth, FinishDay) ? Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value : room.RoomFinishDate;


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

                ViewBag.Weeks = Need.SelectList.getWeeks(true, Week);
                DayOfWeek? dayofWeek = null;
                if(!string.IsNullOrEmpty(Week))
                {
                    dayofWeek = Need.Codes.getDayofWeek(Week);
                }

                IEnumerable<RoomDetail> roomDetails = (new RoomDetailOp()).GetRoomDetails(id, q, start, finish, dayofWeek).OrderBy(p => p.RoomDetailDate).ThenBy(p => p.RoomDetailTimeStart);

                RoomProperties roomProperties = new RoomProperties();
                //roomProperties.Degrees = (new DegreeOp()).GetDegrees();
                //roomProperties.Grades = (new GradeOp()).GetGrades();
                //roomProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
                roomProperties.RoomTypes = (new RoomTypeOp()).GetRoomTypes();
                int skip = (pagenumber - 1) * CountRow;
                roomProperties.RoomDetails = roomDetails.Skip(skip).Take(CountRow).ToList();
                roomProperties.Rooms = rooms.ToList();

                CalendarOp calendarOp = new CalendarOp();
                List<Calendar> calendars = calendarOp.GetCalendars(room.RoomStartDate, room.RoomFinishDate);
                ViewBag.Holidays = calendars;

                ViewData["Title"] = " برنامه ریزی "  + room.RoomType.RoomTypeTitle + " " + room.RoomTitle;
                ViewData["MaxRows"] = roomDetails.Count();
                ViewData["CountRow"] = CountRow;

                return View(roomProperties);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }


        public IActionResult New(int id, int roomid, int week, string day = "", string StartHour = "01", string StartMinute = "01", string FinishHour = "01", string FinishMinute = "01", string returnUrl = "")
        {
            string Title = "برنامه جدید";

            try
            {
                var room = (new RoomOp()).GetRoom(roomid);
                ViewData["TeacherCourse"] = (room.RoomTypeId == 1);
                ViewData["Week"] = week;

                if(room.RoomFinishDate < Need.Codes.GetLocalDateTime())
                {
                    string msg = "زمان این کلاس گذشته است و امکان برنامه ریزی جدید وجود ندارد";
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Error";
                    return Redirect(returnUrl);
                }

                if (id > 0)
                {
                    RoomDetailOp roomDetailOp = new RoomDetailOp();
                    var roomDetail = roomDetailOp.GetRoomDetail(id);
                    if (roomDetail == null)
                    {
                        return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                    }
                    else
                    {
                        if (week == 0)
                        {
                            string[] persiandate = Need.Codes.getPersianDate(roomDetail.RoomDetailDate).Split("/");
                            string Day = persiandate[2];
                            string Month = persiandate[1];
                            string Year = persiandate[0];
                            ViewData["Year"] = Year;
                            ViewBag.Days = Need.SelectList.getDays(Day);
                            ViewBag.Months = Need.SelectList.getMonths(Month);                            
                        }
                        else
                        {
                            string persianday = Need.Codes.getPersianDay(roomDetail.RoomDetailDate);
                            ViewBag.Weeks = ViewBag.Weeks = Need.SelectList.getWeeks(false, persianday);
                        }
                        ViewBag.CloseOnTime = Need.SelectList.getBoolean(roomDetail.CloseOnTime.ToString());
                        string[] starth = roomDetail.RoomDetailTimeStart.Split(':');
                        ViewBag.StartHours = Need.SelectList.getHours(starth[0]);
                        ViewBag.StartMinutes = Need.SelectList.getMinutes(starth[1]);
                        string[] finishh = roomDetail.RoomDetailTimeFinish.Split(':');
                        ViewBag.FinishHours = Need.SelectList.getHours(finishh[0]);
                        ViewBag.FinishMinutes = Need.SelectList.getMinutes(finishh[1]);
                        ViewBag.TeacherCourses = Need.SelectList.getTeacherCourses(room.DegreeId, room.GradeId, room.StudyFieldId, roomDetail.CourseId, roomDetail.UserId);

                        Title = " ویرایش برنامه " + room.RoomType.RoomTypeTitle + " در روز " + Need.Codes.getPersianDay(roomDetail.RoomDetailDate) + " مورخ " 
                            + Need.Codes.getPersianDate(roomDetail.RoomDetailDate) + " از ساعت " +  roomDetail.RoomDetailTimeStart + " تا ساعت " + roomDetail.RoomDetailTimeFinish;
                        ViewData["Title"] = Title;
                        ViewData["returnUrl"] = returnUrl;
                        return View("RoomDetailNew", roomDetail);
                    }
                }
                else
                {
                    if (week == 0)
                    {
                        string[] persiandate = Need.Codes.getPersianDate().Split("/");
                        string Day = persiandate[2];
                        string Month = persiandate[1];
                        string Year = persiandate[0];
                        ViewData["Year"] = Year;
                        ViewBag.Days = Need.SelectList.getDays(Day);
                        ViewBag.Months = Need.SelectList.getMonths(Month);
                    }
                    else
                    {
                        day = string.IsNullOrEmpty(day) ? Need.Codes.getDay(0) : day;
                        ViewBag.Weeks = ViewBag.Weeks = Need.SelectList.getWeeks(false, day);
                    }
                    ViewBag.CloseOnTime = Need.SelectList.getBoolean();
                    ViewBag.StartHours = Need.SelectList.getHours(StartHour);
                    ViewBag.StartMinutes = Need.SelectList.getMinutes(StartMinute);
                    ViewBag.FinishHours = Need.SelectList.getHours(FinishHour);
                    ViewBag.FinishMinutes = Need.SelectList.getMinutes(FinishMinute);
                    ViewBag.TeacherCourses = Need.SelectList.getTeacherCourses(room.DegreeId, room.GradeId, room.StudyFieldId, null, null);

                    RoomDetail roomDetail = new RoomDetail();
                    roomDetail.RoomId = roomid;
                    roomDetail.Room = room;
                    ViewData["Title"] = Title + " برای " + room.RoomType.RoomTypeTitle + " " + room.RoomTitle + " از تاریخ " + Need.Codes.getPersianDate(room.RoomStartDate) + " تا تاریخ " + Need.Codes.getPersianDate(room.RoomFinishDate);
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomDetailNew", roomDetail);
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Newed(int id, int roomid, string comment, bool closeOnTime, string StartHour, string StartMinute, string FinishHour, string FinishMinute, string teacherCourse, string Day, string Month, string Year, string Week, bool AllowHoliday, string returnUrl = "")
        {
            try
            {
                if (!Checks.CheckPersianTime(StartHour + ":" + StartMinute))
                {
                    return Content(Need.Codes.GetResource("InvalidTime").Replace("N", "شروع"));
                }
                if (!Checks.CheckPersianTime(FinishHour + ":" + FinishMinute))
                {
                    return Content(Need.Codes.GetResource("InvalidTime").Replace("N", "پایان"));
                }
                if ((StartHour + ":" + StartMinute).CompareTo(FinishHour + ":" + FinishMinute) >= 0)
                {
                    return Content("ساعت شروع نباید بعد از ساعت پایان باشد");
                }

                var room = (new RoomOp()).GetRoom(roomid);
                DateTime RoomDetailDate = Need.Codes.GetLocalDateTime();
                
                if (string.IsNullOrEmpty(Week))
                {
                    if (!Checks.CheckPersianDate(Year, Month, Day))
                    {
                        return Content(Need.Codes.GetResource("InvalidDate").Replace("N", "برنامه"));
                    }
                    RoomDetailDate = Need.Codes.PersianDateToDate(Year, Month, Day).Value;
                    if (RoomDetailDate.CompareTo(room.RoomStartDate) < 0 || RoomDetailDate.CompareTo(room.RoomFinishDate) > 0)
                    {
                        return Content("تاریخ برنامه باید در محدوده تاریخ کلاس باشد");
                    }
                }
                if(room.RoomTypeId == 1)
                {
                    if (string.IsNullOrEmpty(teacherCourse))
                    {
                        return Content("مدرس و درس مشخص نشده است");
                    }
                }                     

                RoomDetailOp roomDetailOp = new RoomDetailOp();
                if (id > 0)
                { 
                
                    var ExistRoomDetailId = roomDetailOp.GetRoomDetailId(roomid, RoomDetailDate, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute, id);
                    if(ExistRoomDetailId > 0)
                    {
                        return Content("برای " + Need.Codes.getPersianDate(RoomDetailDate) + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute + " برنامه ریزی شده است ");
                    }

                    var roomDetail = roomDetailOp.GetRoomDetail(id);
                    if (roomDetail == null)
                    {
                        return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                    }

                    if(room.RoomTypeId == 1)
                    {
                        string[] tc = teacherCourse.Split('-');
                        var ExistRoomDetailId2 = roomDetailOp.GetRoomDetailId(RoomDetailDate, Convert.ToInt32(tc[0]), StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute, id);
                        if (ExistRoomDetailId2 > 0)
                        {
                            return Content("برای این معلم در تاریخ " + Need.Codes.getPersianDate(RoomDetailDate) + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute + " برنامه ریزی شده است ");
                        }
                        roomDetail.UserId = Convert.ToInt32(tc[0]);
                        roomDetail.CourseId = Convert.ToInt32(tc[1]);
                    }
                    else
                    {
                        roomDetail.UserId = null;
                        roomDetail.CourseId = null;
                    }
                    roomDetail.RoomDetailDate = RoomDetailDate;
                    roomDetail.RoomDetailTimeStart = StartHour + ":" + StartMinute;
                    roomDetail.RoomDetailTimeFinish = FinishHour + ":" + FinishMinute;                    
                    roomDetail.RoomDetailId = id;
                    roomDetail.CloseOnTime = closeOnTime;
                    roomDetail.Comment = comment;

                    string count = await roomDetailOp.UpdateRoomDetail(roomDetail);
                    string msg = " ویرایش برنامه روز " + Need.Codes.getPersianDay(roomDetail.RoomDetailDate) + " مورخ " + Need.Codes.getPersianDate(roomDetail.RoomDetailDate) + " از ساعت " +
                                roomDetail.RoomDetailTimeStart + " تا ساعت " + roomDetail.RoomDetailTimeFinish + " با موفقیت انجام شد ";
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
                }
                else
                {
                    if (string.IsNullOrEmpty(Week))
                    {
                        RoomDetail roomDetail = new RoomDetail();
                        if (room.RoomTypeId == 1)
                        {
                            string[] tc = teacherCourse.Split('-');
                            var ExistRoomDetailId2 = roomDetailOp.GetRoomDetailId(RoomDetailDate, Convert.ToInt32(tc[0]), StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute);
                            if (ExistRoomDetailId2 > 0)
                            {
                                return Content("برای این معلم در تاریخ " + Need.Codes.getPersianDate(RoomDetailDate) + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute + " برنامه ریزی شده است ");
                            }
                            roomDetail.UserId = Convert.ToInt32(tc[0]);
                            roomDetail.CourseId = Convert.ToInt32(tc[1]);
                        }
                        else
                        {
                            roomDetail.UserId = null;
                            roomDetail.CourseId = null;
                        }
                        roomDetail.RoomDetailDate = RoomDetailDate;
                        roomDetail.RoomDetailTimeStart = StartHour + ":" + StartMinute;
                        roomDetail.RoomDetailTimeFinish = FinishHour + ":" + FinishMinute;
                        roomDetail.RoomDetailId = id;
                        roomDetail.RoomId = roomid;
                        roomDetail.CloseOnTime = closeOnTime;
                        roomDetail.Comment = comment;

                        if (!AllowHoliday)
                        {
                            CalendarOp calendarOp = new CalendarOp();
                            bool IsHoliday = calendarOp.IsHoliday(RoomDetailDate);
                            if(IsHoliday)
                            {
                                string pdate = Need.Codes.getPersianDate(RoomDetailDate);
                                string pday = Need.Codes.getPersianDay(RoomDetailDate);
                                return Content("روز " + pday + " موزخ "  + pdate + " تعطیل می باشد ");
                            }
                        }

                        var ExistRoomDetailId = roomDetailOp.GetRoomDetailId(roomid, RoomDetailDate, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute);
                        if(ExistRoomDetailId > 0)
                        {
                            return Content("برای " + Need.Codes.getPersianDate(RoomDetailDate) + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute + " برنامه ریزی شده است ");
                        }

                        string count = await roomDetailOp.InsertRoomDetail(roomDetail);
                        string msg = "  برنامه روز " + Need.Codes.getPersianDay(roomDetail.RoomDetailDate) + " مورخ " + Need.Codes.getPersianDate(roomDetail.RoomDetailDate) + " از ساعت " +
                                    roomDetail.RoomDetailTimeStart + " تا ساعت " + roomDetail.RoomDetailTimeFinish + " با موفقیت ایجاد شد ";
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Content("ok," + returnUrl);
                    }
                    else
                    {
                        int num = 0;

                        DayOfWeek day_of_week = Need.Codes.getDayofWeek(Week);
                        DateTime StartDate = Need.Codes.GetLocalDateTime().Date.CompareTo(room.RoomStartDate.Date) >= 0 ? Need.Codes.GetLocalDateTime().Date : room.RoomStartDate.Date;
                        DateTime FinishDate = room.RoomFinishDate.Date;
                        for (DateTime dt = StartDate.Date; dt.Date.CompareTo(FinishDate.Date) <= 0; dt = dt.AddDays(1).Date)
                        {
                            if(dt.DayOfWeek == day_of_week)
                            {
                                RoomDetail roomDetail = new RoomDetail();
                                var ExistRoomDetailId2 = 0;
                                if (room.RoomTypeId == 1)
                                {
                                    string[] tc = teacherCourse.Split('-');
                                    ExistRoomDetailId2 = roomDetailOp.GetRoomDetailId(dt, Convert.ToInt32(tc[0]), StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute);                                    
                                    roomDetail.UserId = Convert.ToInt32(tc[0]);
                                    roomDetail.CourseId = Convert.ToInt32(tc[1]);
                                }
                                else
                                {
                                    roomDetail.UserId = null;
                                    roomDetail.CourseId = null;
                                }
                                roomDetail.RoomDetailDate = dt;
                                roomDetail.RoomDetailTimeStart = StartHour + ":" + StartMinute;
                                roomDetail.RoomDetailTimeFinish = FinishHour + ":" + FinishMinute;
                                roomDetail.RoomDetailId = id;
                                roomDetail.RoomId = roomid;
                                roomDetail.Comment = comment;

                                bool AllowAdd = true;
                                if(!AllowHoliday)
                                {
                                    CalendarOp calendarOp = new CalendarOp();
                                    var Holiday = calendarOp.IsHoliday(dt);
                                    AllowAdd = !Holiday;
                                }
                                                                
                                var ExistRoomDetailId = roomDetailOp.GetRoomDetailId(roomid, dt, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute);                       

                                if (ExistRoomDetailId == 0 && ExistRoomDetailId2 == 0 && AllowAdd)
                                {
                                    string count = await roomDetailOp.InsertRoomDetail(roomDetail);
                                    if(!count.StartsWith("Error"))
                                    {
                                        num++;
                                    }
                                }                                
                            }
                        }

                        
                        string msg = " تعداد " + num.ToString() + "  برنامه برای روزهای " + Week + " از ساعت " +
                                    StartHour + ":" + StartMinute + " تا ساعت " + FinishHour + ":" + FinishMinute + " با موفقیت ایجاد شد ";
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Success";
                        return Content("ok," + returnUrl);
                    }
                }
            }
            catch(Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }       

        public IActionResult Week(int id)
        {
            try
            {
                var room = (new RoomOp()).GetRoom(id);

                RoomDetailOp roomDetailOp = new RoomDetailOp();
                List<RoomDetailGrouped> roomDetailGroupeds = roomDetailOp.GetRoomDetailGrouped(id);
                roomDetailGroupeds = roomDetailGroupeds.GroupBy(x => new { x.TimeStr, x.DayStr, x.CourseName, x.TeacherName, x.Comment })
                    .Select(p => new RoomDetailGrouped
                    {
                        TimeStr = p.Key.TimeStr,
                        DayStr = p.Key.DayStr,
                        CourseName = p.Key.CourseName,
                        TeacherName = p.Key.TeacherName,
                        Comment = p.Key.Comment,
                    }).ToList();

                ViewBag.StartHours = Need.SelectList.getHours();
                ViewBag.StartMinutes = Need.SelectList.getMinutes();
                ViewBag.FinishHours = Need.SelectList.getHours();
                ViewBag.FinishMinutes = Need.SelectList.getMinutes();

                ViewData["RoomId"] = id;
                ViewData["RoomTypeId"] = room.RoomTypeId;
                ViewData["Title"] = " برنامه " + room.RoomType.RoomTypeTitle + " " + room.RoomTitle + " از تاریخ " + Need.Codes.getPersianDate(room.RoomStartDate) + " تا تاریخ " + Need.Codes.getPersianDate(room.RoomFinishDate);
                return View("Week", roomDetailGroupeds);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public IActionResult RoomDetailShow(int id, string Day, string StartHour, string StartMinute, string FinishHour, string FinishMinute, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();
            RoomOp roomOp = new RoomOp();
            try
            {
                var room = roomOp.GetRoom(id);
                if (room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                ViewData["RoomId"] = id;
                List<DateTime> Datetimes = new List<DateTime>();
                DayOfWeek day_of_week = Need.Codes.getDayofWeek(Day);
                DateTime StartDate = room.RoomStartDate.Date;
                DateTime FinishDate = room.RoomFinishDate.Date;
                for (DateTime dt = StartDate.Date; dt.Date.CompareTo(FinishDate.Date) <= 0; dt = dt.AddDays(1).Date)
                {
                    if (dt.DayOfWeek == day_of_week)
                    {
                        Datetimes.Add(dt);
                    }
                }

                List<RoomDetail> roomDetails = roomDetailOp.GetRoomDetails(id, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute, Datetimes).ToList();
                if (roomDetails.Count() > 0)
                {
                    CalendarOp calendarOp = new CalendarOp();
                    List<Calendar> calendars = calendarOp.GetCalendars(room.RoomStartDate, room.RoomFinishDate, day_of_week);
                    ViewBag.Holidays = calendars;
                    foreach(Calendar calendar in calendars)
                    {
                        var Exist = roomDetails.Any(p => p.RoomDetailDate.Date == calendar.CalendarDate.Date);
                        if(!Exist)
                        {
                            roomDetails.Add(new RoomDetail { RoomDetailDate = calendar.CalendarDate, Comment = "", Room = room, RoomId = room.RoomId, RoomDetailTimeStart = null, RoomDetailTimeFinish = null, UserId = null, CourseId = null });
                        }
                    }
                    
                    ViewData["Title"] = " برنامه های " + Day + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute + 
                        " در کلاس " + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomDetailShow", roomDetails.OrderBy(p => p.RoomDetailDate).ThenBy(p => p.RoomDetailTimeStart));
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RoomDetailDelete(string ids, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }

                IEnumerable<RoomDetail> roomDetails = roomDetailOp.GetRoomDetails(ids2);
                if (roomDetails.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomDetailDelete", roomDetails);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RoomDetailDelete2(int id, string Day, string StartHour, string StartMinute, string FinishHour, string FinishMinute, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();
            RoomOp roomOp = new RoomOp();
            try
            {
                var room = roomOp.GetRoom(id);
                if (room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                List<DateTime> Datetimes = new List<DateTime>();
                DayOfWeek day_of_week = Need.Codes.getDayofWeek(Day);
                DateTime StartDate = room.RoomStartDate.AddDays(-1);
                DateTime FinishDate = room.RoomFinishDate.AddDays(1);
                for (DateTime dt = StartDate; dt.CompareTo(FinishDate) <= 0; dt = dt.AddDays(1))
                {
                    if (dt.DayOfWeek == day_of_week)
                    {
                        Datetimes.Add(dt);
                    }
                }

                IEnumerable<RoomDetail> roomDetails = roomDetailOp.GetRoomDetails(id, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute, Datetimes);
                if (roomDetails.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("RoomDetailDelete", roomDetails);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoomDetailDeleted(string ids, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }

                List<RoomDetail> roomDetails = new List<RoomDetail>();
                foreach (var id in ids2)
                {
                    RoomDetail roomDetail = roomDetailOp.GetRoomDetail(id);
                    if (roomDetail != null)
                    {
                        roomDetails.Add(roomDetail);
                    }
                }

                if (roomDetails.Count() > 0)
                {
                    int count = await roomDetailOp.DeleteRoomDetails(roomDetails);
                    string msg = Need.Codes.GetResource("ItemsDelete");
                    msg = string.Format(msg, count);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
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